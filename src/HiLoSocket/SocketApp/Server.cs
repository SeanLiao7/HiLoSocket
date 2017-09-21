using System;
using System.ComponentModel.DataAnnotations;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using HiLoSocket.Extension;
using HiLoSocket.Logger;
using HiLoSocket.Model;

namespace HiLoSocket.SocketApp
{
    public class Server<TCommandModel> : SocketBase<TCommandModel>
        where TCommandModel : class
    {
        /// <summary>
        /// The maximum pending connection length
        /// </summary>
        public const int MaxPendingConnectionLength = 100;

        private readonly ManualResetEventSlim _allDone = new ManualResetEventSlim( );
        private readonly object _listenerLock = new object( );
        private Socket _listener;

        /// <summary>
        /// Gets a value indicating whether this instance is disposed.
        /// </summary>
        /// <value>
        ///   <c>true</c> if this instance is disposed; otherwise, <c>false</c>.
        /// </value>
        public bool IsDisposed { get; private set; }

        /// <summary>
        /// Gets a value indicating whether this instance is listening.
        /// </summary>
        /// <value>
        ///   <c>true</c> if this instance is listening; otherwise, <c>false</c>.
        /// </value>
        public bool IsListening { get; private set; }

        /// <summary>
        /// Gets the local ip end point.
        /// </summary>
        /// <value>
        /// The local ip end point.
        /// </value>
        public IPEndPoint LocalIpEndPoint { get; }

        /// <inheritdoc />
        /// <summary>
        /// Initializes a new instance of the <see cref="T:HiLoSocket.SocketApp.Server`1" /> class.
        /// </summary>
        /// <param name="serverConfigModel">The server model.</param>
        public Server( ServerConfigModel serverConfigModel )
            : this( serverConfigModel, null )
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Server{TCommandModel}"/> class.
        /// </summary>
        /// <param name="serverConfigModel">The server model.</param>
        /// <param name="logger">The logger.</param>
        /// <exception cref="ArgumentNullException">serverConfigModel</exception>
        /// <exception cref="ValidationException"></exception>
        public Server( ServerConfigModel serverConfigModel, ILogger logger )
            : base( serverConfigModel?.FormatterType, serverConfigModel?.CompressType, logger )
        {
            CheckIfNullInput( serverConfigModel );
            ValidateInputModel( serverConfigModel );
            LocalIpEndPoint = serverConfigModel?.LocalIpEndPoint;
        }

        /// <summary>
        /// Starts the listening.
        /// </summary>
        /// <exception cref="ObjectDisposedException">TCommandModel - Server 已經被 Dispose 囉</exception>
        /// <exception cref="InvalidOperationException">伺服器監聽用戶端失敗，詳細資訊請參照 Inner Exception。</exception>
        public void StartListening( )
        {
            CheckIfDisposed( );

            if ( CheckIfIsListening( ) )
                return;

            var listener = new Socket( AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp );
            _listener = listener;

            try
            {
                TryListen( listener );
            }
            catch ( Exception e )
            {
                Logger?.Log( new LogModel
                {
                    Time = DateTime.Now,
                    Message = $"伺服器監聽用戶端失敗, 伺服器 : {LocalIpEndPoint}, 物件名稱 : {ToString( )}, 例外訊息 : {e.Message}"
                } );

                throw new InvalidOperationException( $@"伺服器監聽用戶端失敗，詳細資訊請參照 Inner Exception。
Inner Exeption 訊息 : {e.Message}", e );
            }
            finally
            {
                StopListening( listener );
            }
        }

        /// <summary>
        /// Stops the listening.
        /// </summary>
        /// <exception cref="ObjectDisposedException">物件已經被 Dispose 囉</exception>
        public void StopListening( )
        {
            CheckIfDisposed( );
            StopListening( _listener );
        }

        public override string ToString( )
        {
            if ( IsDisposed )
                throw new ObjectDisposedException( nameof( Server<TCommandModel> ), "Server 已經被 Dispose 囉" );

            return $"Server with {typeof( TCommandModel )} type model";
        }

        protected override void Dispose( bool disposing )
        {
            if ( !IsDisposed )
            {
                if ( disposing )
                {
                    _allDone?.Dispose( );
                    IsListening = false;
                }

                _listener?.Close( );
            }

            IsDisposed = true;
            base.Dispose( disposing );
        }

        protected override void ReceiveCommandModelCallback( IAsyncResult asyncResult )
        {
            base.ReceiveCommandModelCallback( asyncResult );
            if ( asyncResult.AsyncState is StateObjectModel state )
            {
                var handler = state.WorkSocket;
                var commandModel = GetCommandModel( state );
                Send( handler, commandModel );
                InvokeOnSocketCommandModelReceived( commandModel );
            }
        }

        private static void CheckIfNullInput( ServerConfigModel serverConfigModel )
        {
            if ( serverConfigModel == null )
            {
                throw new ArgumentNullException( nameof( serverConfigModel ),
                    $@"時間 : {DateTime.Now.GetDateTimeString( )},
類別 : {nameof( Server<TCommandModel> )},
方法 : Constructor,
內容 : 你沒初始化 {nameof( serverConfigModel )} 喔。" );
            }
        }

        private static void ValidateInputModel( ServerConfigModel serverConfigModel )
        {
            if ( serverConfigModel.ValidateObject( out var errorMessages ) == false )
            {
                throw new ValidationException( $@"時間 : {DateTime.Now.GetDateTimeString( )},
類別 : {nameof( Server<TCommandModel> )},
方法 : Constructor,
內容 : {string.Join( "\n", errorMessages )}" );
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
                    var state = new StateObjectModel
                    {
                        WorkSocket = handler
                    };

                    Logger?.Log( new LogModel
                    {
                        Time = DateTime.Now,
                        Message = $"伺服器已接受用戶連線, 伺服器 : {handler.LocalEndPoint}, 用戶端 : {handler.RemoteEndPoint}"
                    } );

                    handler.BeginReceive( state.Buffer, 0, StateObjectModel.DataInfoSize, 0, ReadTotalLengthCallback, state );
                }
                catch ( ObjectDisposedException e )
                {
                    Logger?.Log( new LogModel
                    {
                        Time = DateTime.Now,
                        Message = $"伺服器已關閉, 伺服器：{LocalIpEndPoint}, 物件名稱 : {ToString( )}, 例外訊息 : {e.Message}"
                    } );
                }
            }
        }

        private void CheckIfDisposed( )
        {
            if ( IsDisposed )
                throw new ObjectDisposedException( nameof( Server<TCommandModel> ), "Server 已經被 Dispose 囉" );
        }

        private bool CheckIfIsListening( )
        {
            lock ( _listenerLock )
            {
                if ( IsListening )
                    return true;

                IsListening = true;
            }
            return false;
        }

        private void SetupListener( Socket listener )
        {
            listener.Bind( LocalIpEndPoint );
            listener.Listen( MaxPendingConnectionLength );
        }

        private void StopListening( Socket listener )
        {
            listener?.Close( );
            IsListening = false;
        }

        private void TryAcceptClient( Socket listener )
        {
            _allDone.Reset( );

            Logger?.Log( new LogModel
            {
                Time = DateTime.Now,
                Message = $"伺服器等待連線中, 伺服器 : {LocalIpEndPoint}"
            } );

            listener.BeginAccept( AcceptCallback, listener );
            _allDone.Wait( );
        }

        private void TryListen( Socket listener )
        {
            SetupListener( listener );
            while ( IsListening )
            {
                CheckIfDisposed( );
                TryAcceptClient( listener );
            }
        }

        ~Server( )
        {
            Dispose( false );
        }
    }
}