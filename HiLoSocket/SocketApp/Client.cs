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
    public class Client<TCommandModel> : SocketBase<TCommandModel>
        where TCommandModel : class
    {
        private readonly ManualResetEventSlim _connectDone = new ManualResetEventSlim( );
        private readonly ManualResetEventSlim _receiveDone = new ManualResetEventSlim( );
        private readonly ManualResetEventSlim _sendDone = new ManualResetEventSlim( );

        /// <summary>
        /// Gets a value indicating whether this instance is disposed.
        /// </summary>
        /// <value>
        ///   <c>true</c> if this instance is disposed; otherwise, <c>false</c>.
        /// </value>
        public bool IsDisposed { get; private set; }

        /// <summary>
        /// Gets the local ip end point.
        /// </summary>
        /// <value>
        /// The local ip end point.
        /// </value>
        public IPEndPoint LocalIpEndPoint { get; }

        /// <summary>
        /// Gets the remote ip end point.
        /// </summary>
        /// <value>
        /// The remote ip end point.
        /// </value>
        public IPEndPoint RemoteIpEndPoint { get; }

        /// <summary>
        /// Initializes a new instance of the <see cref="Client{TModel}"/> class.
        /// </summary>
        /// <param name="clientModel">The client model.</param>
        public Client( ClientModel clientModel )
            : this( clientModel, null )
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Client{TCommandModel}"/> class.
        /// </summary>
        /// <param name="clientModel">The client model.</param>
        /// <param name="logger">The logger.</param>
        /// <exception cref="ArgumentNullException">clientModel</exception>
        /// <exception cref="ValidationException"></exception>
        public Client( ClientModel clientModel, ILogger logger )
            : base( clientModel?.FormatterType, logger )
        {
            if ( clientModel == null )
            {
                throw new ArgumentNullException( nameof( clientModel ),
                    $@"時間 : {DateTime.Now.GetDateTimeString( )},
類別 : {nameof( Client<TCommandModel> )},
方法 : Constructor,
內容 : 你沒初始化 {nameof( clientModel )} 喔。" );
            }

            if ( clientModel.ValidateObject( out var errorMessages ) == false )
            {
                throw new ValidationException(
                    $@"時間 : {DateTime.Now.GetDateTimeString( )},
類別 : {nameof( Client<TCommandModel> )},
方法 : Constructor,
內容 : {string.Join( "\n", errorMessages )}" );
            }

            LocalIpEndPoint = clientModel.LocalIpEndPoint;
            RemoteIpEndPoint = clientModel.RemoteIpEndPoint;
        }

        /// <summary>
        /// Sends the specified command model.
        /// </summary>
        /// <param name="commandModel">The command model.</param>
        /// <exception cref="ObjectDisposedException">TCommandModel - Client 已經被 Dispose 囉</exception>
        /// <exception cref="ArgumentNullException">commandModel - 沒東西可以傳送喔，請記得初始化資料物件。</exception>
        /// <exception cref="InvalidOperationException">客戶端傳送訊息至伺服器失敗，詳細請參照 Inner Exception。</exception>
        public void Send( TCommandModel commandModel )
        {
            if ( IsDisposed )
                throw new ObjectDisposedException( nameof( Client<TCommandModel> ), "Client 已經被 Dispose 囉" );

            if ( commandModel == null )
                throw new ArgumentNullException( nameof( commandModel ), "沒東西可以傳送喔，請記得初始化資料物件。" );

            var client = new Socket( AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp );

            try
            {
                client.BeginConnect( RemoteIpEndPoint, ConnectCallback, client );
                _connectDone.Wait( );

                Send( client, commandModel );
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

                throw new InvalidOperationException( $@"客戶端傳送訊息至伺服器失敗，詳細請參照 Inner Exception。
Inner Execption 訊息 : {e.Message}", e );
            }
        }

        public override string ToString( )
        {
            if ( IsDisposed )
                throw new ObjectDisposedException( nameof( Client<TCommandModel> ), "Client 已經被 Dispose 囉" );

            return $"Client with {nameof( TCommandModel )} model";
        }

        protected override void Dispose( bool disposing )
        {
            if ( !IsDisposed )
            {
                if ( disposing )
                {
                    _connectDone?.Dispose( );
                    _receiveDone?.Dispose( );
                    _sendDone?.Dispose( );
                }
            }

            IsDisposed = true;
            base.Dispose( disposing );
        }

        protected override void ReceiveCommandModelCallback( IAsyncResult asyncResult )
        {
            base.ReceiveCommandModelCallback( asyncResult );

            if ( asyncResult.AsyncState is StateObjectModel state )
            {
                Close( state );
                var commandModel = GetCommandModel( state );
                InvokeOnSocketCommandModelRecieved( commandModel );
            }

            _receiveDone.Set( );
        }

        protected override void SendCallback( IAsyncResult asyncResult )
        {
            base.SendCallback( asyncResult );
            _sendDone.Set( );
        }

        private void Close( StateObjectModel state )
        {
            var handler = state.WorkSocket;
            var remoteIp = handler.RemoteEndPoint;
            var localIp = handler.LocalEndPoint;

            try
            {
                handler.Shutdown( SocketShutdown.Both );
                handler.Close( );

                Logger?.Log( new LogModel
                {
                    LogTime = DateTime.Now,
                    LogMessage = $"用戶端已關閉與伺服器連線, 伺服器 : {remoteIp}, 用戶端 : {localIp}"
                } );
            }
            catch ( Exception )
            {
                Logger?.Log( new LogModel
                {
                    LogTime = DateTime.Now,
                    LogMessage = $"用戶端關閉連線失敗囉, 伺服器 : {remoteIp}, 用戶端 : {localIp}"
                } );
            }
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
                var state = new StateObjectModel
                {
                    WorkSocket = client
                };

                client.BeginReceive( state.Buffer, 0, StateObjectModel.DataInfoSize, 0, ReadTotalLengthCallback, state );
            }
            catch ( Exception e )
            {
                Logger?.Log( new LogModel
                {
                    LogTime = DateTime.Now,
                    LogMessage = $"資料接收失敗啦, 傳送端 : {client.RemoteEndPoint}, 接收端 : {client.LocalEndPoint}, 例外訊息 : {e.Message}"
                } );

                throw new InvalidOperationException( $@"客戶端接收伺服器訊息失敗，詳細請參照 Inner Exception。
Inner Execption 訊息 : {e.Message}", e );
            }
        }
    }
}