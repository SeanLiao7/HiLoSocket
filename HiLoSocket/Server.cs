using System;
using System.ComponentModel.DataAnnotations;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using HiLoSocket.Logger;
using HiLoSocket.Model;

namespace HiLoSocket
{
    public class Server<TModel> : SocketBase<TModel> where TModel : class
    {
        public const int MaxPendingConnectionLength = 4;
        private readonly ManualResetEventSlim _allDone = new ManualResetEventSlim( );
        private bool _isDisposed;
        private bool _isListening;
        private Socket _listener;
        public IPEndPoint LocalIpEndPoint { get; }

        public Server( ServerModel serverModel )
            : this( serverModel, null )
        {
        }

        public Server( ServerModel serverModel, ILogger logger )
            : base( serverModel?.FormatterType, logger )
        {
            if ( serverModel == null )
            {
                throw new ArgumentNullException( nameof( serverModel ),
                    $@"時間 : {DateTime.Now.GetDateTimeString( )},
類別 : {nameof( Server<TModel> )},
方法 : Constructor,
內容 : 你沒初始化 {nameof( serverModel )} 喔。" );
            }

            if ( serverModel.ValidateObject( out var errorMessages ) == false )
            {
                throw new ValidationException( $@"時間 : {DateTime.Now.GetDateTimeString( )},
類別 : {nameof( Server<TModel> )},
方法 : Constructor,
內容 : {string.Join( "\n", errorMessages )}" );
            }

            LocalIpEndPoint = serverModel.LocalIpEndPoint;
        }

        public void StartListening( )
        {
            if ( _isDisposed )
                throw new ObjectDisposedException( nameof( Server<TModel> ), ToString( ) );

            if ( _isListening )
                return;

            _listener = new Socket( AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp );
            _isListening = true;

            try
            {
                _listener.Bind( LocalIpEndPoint );
                _listener.Listen( MaxPendingConnectionLength );

                while ( _isListening )
                {
                    _allDone.Reset( );

                    Logger?.Log( new LogModel
                    {
                        LogTime = DateTime.Now,
                        LogMessage = $"伺服器等待連線中, 伺服器 : {LocalIpEndPoint}"
                    } );

                    _listener.BeginAccept( AcceptCallback, _listener );
                    _allDone.Wait( );
                }
            }
            catch ( Exception e )
            {
                Logger?.Log( new LogModel
                {
                    LogTime = DateTime.Now,
                    LogMessage = $"伺服器監聽用戶端失敗, 伺服器 : {LocalIpEndPoint}, 例外訊息 : {e.Message}"
                } );

                throw new InvalidOperationException( "伺服器監聽用戶端失敗，詳細資訊請參照 Inner Exception。", e );
            }
        }

        public void StopListening( )
        {
            if ( _isDisposed )
                throw new ObjectDisposedException( nameof( Server<TModel> ), ToString( ) );

            _isListening = false;
            _listener?.Close( );
        }

        public override string ToString( )
        {
            return $"Server with {nameof( TModel )} model";
        }

        protected override void Dispose( bool disposing )
        {
            if ( !_isDisposed )
            {
                if ( disposing )
                {
                    _allDone?.Dispose( );
                }

                _listener?.Close( );
            }

            _isDisposed = true;
            base.Dispose( disposing );
        }

        protected override void ReadModel( IAsyncResult asyncResult )
        {
            base.ReadModel( asyncResult );
            if ( asyncResult.AsyncState is StateObject state )
            {
                var handler = state.WorkSocket;
                var model = default( TModel );

                try
                {
                    model = CommandFormatter.Deserialize<TModel>( state.Buffer );
                }
                catch ( Exception e )
                {
                    Logger?.Log( new LogModel
                    {
                        LogTime = DateTime.Now,
                        LogMessage = $"反序列化失敗囉, 例外訊息 : {e.Message}"
                    } );
                }

                Send( handler, model );
                InvokeOnSocketCommandModelRecieved( model );
            }
        }

        protected override void SendCallback( IAsyncResult asyncResult )
        {
            base.SendCallback( asyncResult );
            try
            {
                if ( asyncResult.AsyncState is Socket handler )
                {
                    var remoteIp = handler.RemoteEndPoint;
                    var localIp = handler.LocalEndPoint;

                    handler.Shutdown( SocketShutdown.Both );
                    handler.Close( );

                    Logger?.Log( new LogModel
                    {
                        LogTime = DateTime.Now,
                        LogMessage = $"伺服器已關閉用戶端連線, 伺服器 : {localIp}, 用戶端 : {remoteIp}"
                    } );
                }
            }
            catch ( Exception e )
            {
                Logger?.Log( new LogModel
                {
                    LogTime = DateTime.Now,
                    LogMessage = $"伺服器關閉連線失敗, 伺服器：{LocalIpEndPoint}, 例外訊息 : {e.Message}"
                } );
            }
        }

        private void AcceptCallback( IAsyncResult asyncResult )
        {
            _allDone.Set( );

            if ( asyncResult.AsyncState is Socket listener )
            {
                try
                {
                    var handler = listener.EndAccept( asyncResult );
                    var state = new StateObject
                    {
                        WorkSocket = handler
                    };

                    Logger?.Log( new LogModel
                    {
                        LogTime = DateTime.Now,
                        LogMessage = $"伺服器已接受用戶連線, 伺服器 : {handler.LocalEndPoint}, 用戶端 : {handler.RemoteEndPoint}"
                    } );

                    handler.BeginReceive( state.Buffer, 0, StateObject.DataInfoSize, 0, ReadTotalLength, state );
                }
                catch ( ObjectDisposedException e )
                {
                    Logger?.Log( new LogModel
                    {
                        LogTime = DateTime.Now,
                        LogMessage = $"伺服器已關閉, 伺服器：{LocalIpEndPoint}, 例外訊息 : {e.Message}"
                    } );
                }
            }
        }

        ~Server( )
        {
            Dispose( false );
        }
    }
}