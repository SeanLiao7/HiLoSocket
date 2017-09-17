using System;
using System.ComponentModel.DataAnnotations;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using HiLoSocket.Logger;
using HiLoSocket.Model;

namespace HiLoSocket
{
    public class Client<TModel> : SocketBase<TModel> where TModel : class
    {
        private readonly ManualResetEventSlim _connectDone = new ManualResetEventSlim( );
        private readonly ManualResetEventSlim _receiveDone = new ManualResetEventSlim( );
        private readonly ManualResetEventSlim _sendDone = new ManualResetEventSlim( );

        public IPEndPoint LocalIpEndPoint { get; }
        public IPEndPoint RemoteIpEndPoint { get; }

        public Client( ClientModel clientModel )
            : this( clientModel, null )
        {
        }

        public Client( ClientModel clientModel, ILogger logger )
            : base( clientModel?.FormatterType, logger )
        {
            if ( clientModel == null )
            {
                throw new ArgumentNullException( nameof( clientModel ),
                    $@"時間 : {DateTime.Now.GetDateTimeString( )};
類別 : {nameof( Client<TModel> )};
方法 : Constructor;
內容 : 你沒初始化 {nameof( clientModel )} 喔。" );
            }

            if ( clientModel.ValidateObject( out var errorMessages ) == false )
            {
                throw new ValidationException(
                    $@"時間 : {DateTime.Now.GetDateTimeString( )};
類別 : {nameof( Client<TModel> )};
方法 : Constructor;
內容 : {string.Join( "\n", errorMessages )}" );
            }

            LocalIpEndPoint = clientModel.LocalIpEndPoint;
            RemoteIpEndPoint = clientModel.RemoteIpEndPoint;
        }

        public void Send( TModel model )
        {
            var client = new Socket( AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp );

            try
            {
                client.BeginConnect( RemoteIpEndPoint, ConnectCallback, client );
                _connectDone.Wait( );

                Send( client, model );
                _sendDone.Wait( );

                Receive( client );
                _receiveDone.Wait( );
            }
            catch ( Exception e )
            {
                Logger?.Log( new LogModel
                {
                    LogTime = DateTime.Now,
                    LogMessage = $"客戶端資料傳送失敗啦, 傳送端 : {client.LocalEndPoint}, 接收端 : {client.RemoteEndPoint}, 例外訊息 : {e.Message}"
                } );

                throw new InvalidOperationException( "客戶端傳送訊息至伺服器失敗，詳細請參照 Inner Exception。", e );
            }
        }

        public override string ToString( )
        {
            return $"Client with {nameof( TModel )} model";
        }

        protected override void ReadModel( IAsyncResult asyncResult )
        {
            base.ReadModel( asyncResult );

            if ( asyncResult.AsyncState is StateObject state )
            {
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
                finally
                {
                    InvokeOnSocketCommandModelRecieved( model );
                }
            }

            _receiveDone.Set( );
        }

        protected override void SendCallback( IAsyncResult asyncResult )
        {
            base.SendCallback( asyncResult );
            _sendDone.Set( );
        }

        private void ConnectCallback( IAsyncResult asyncResult )
        {
            if ( asyncResult.AsyncState is Socket client )
            {
                try
                {
                    client.EndConnect( asyncResult );
                    Logger?.Log( new LogModel
                    {
                        LogTime = DateTime.Now,
                        LogMessage = $"用戶端已連線至伺服器, 伺服器 : {client.RemoteEndPoint}, 用戶端 : {client.LocalEndPoint}"
                    } );

                    _connectDone.Set( );
                }
                catch ( Exception e )
                {
                    Logger?.Log( new LogModel
                    {
                        LogTime = DateTime.Now,
                        LogMessage = $"客戶端連線伺服器失敗, 伺服器：{RemoteIpEndPoint}, 用戶端 : {LocalIpEndPoint}, 例外訊息 : {e.Message}"
                    } );
                }
            }
        }

        private void Receive( Socket client )
        {
            try
            {
                var state = new StateObject
                {
                    WorkSocket = client
                };

                client.BeginReceive( state.Buffer, 0, StateObject.DataInfoSize, 0, ReadTotalLength, state );
            }
            catch ( Exception e )
            {
                Logger?.Log( new LogModel
                {
                    LogTime = DateTime.Now,
                    LogMessage = $"資料接收失敗啦, 傳送端 : {client.RemoteEndPoint}, 接收端 : {client.LocalEndPoint}, 例外訊息 : {e.Message}"
                } );

                throw new InvalidOperationException( "客戶端接收伺服器訊息失敗，詳細請參照 Inner Exception。", e );
            }
        }
    }
}