using System;
using System.ComponentModel.DataAnnotations;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using HiLoSocket.Extension;
using HiLoSocket.Logger;
using HiLoSocket.Model;
using HiLoSocket.Model.InternalOnly;

namespace HiLoSocket.SocketApp
{
    /// <inheritdoc />
    /// <summary>
    /// Socket Client.
    /// </summary>
    /// <typeparam name="TCommandModel">The type of the command model.</typeparam>
    /// <seealso cref="T:HiLoSocket.SocketApp.SocketBase`1" />
    public sealed class Client<TCommandModel> : SocketBase<TCommandModel>
        where TCommandModel : class
    {
        private readonly ManualResetEventSlim _connectDone = new ManualResetEventSlim( );
        private readonly object _disposedLock = new object( );
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
        /// Gets the timeout time.
        /// </summary>
        /// <value>
        /// The timeout time.
        /// </value>
        public int TimeoutTime { get; }

        /// <inheritdoc />
        /// <summary>
        /// Initializes a new instance of the <see cref="T:HiLoSocket.SocketApp.Client`1" /> class.
        /// </summary>
        /// <param name="clientConfigModel">The client model.</param>
        internal Client( ClientConfigModel clientConfigModel )
            : this( clientConfigModel, null )
        {
        }

        /// <inheritdoc />
        /// <summary>
        /// Initializes a new instance of the <see cref="T:HiLoSocket.SocketApp.Client`1" /> class.
        /// </summary>
        /// <param name="clientConfigModel">The client model.</param>
        /// <param name="logger">The logger.</param>
        /// <exception cref="T:System.ArgumentNullException">clientConfigModel</exception>
        /// <exception cref="T:System.ComponentModel.DataAnnotations.ValidationException"></exception>
        internal Client( ClientConfigModel clientConfigModel, ILogger logger )
            : base( clientConfigModel?.FormatterType, clientConfigModel?.CompressType, logger )
        {
            CheckIfNullModel( clientConfigModel );
            ValidateModel( clientConfigModel );
            LocalIpEndPoint = clientConfigModel?.LocalIpEndPoint;
            RemoteIpEndPoint = clientConfigModel?.RemoteIpEndPoint;
            TimeoutTime = clientConfigModel?.TimeOutTime ?? 3000;
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
            CheckIfDisposed( );
            CheckIfNullInput( commandModel );

            var client = new Socket( AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp );

            lock ( _disposedLock )
            {
                try
                {
                    TrySend( client, commandModel );
                }
                catch ( Exception e )
                {
                    Logger?.Log( new LogModel
                    {
                        Time = DateTime.Now,
                        Message = $"客戶端資料傳送失敗啦, 連線關閉, 物件名稱 : {ToString( )}, 例外訊息 : {e.Message}"
                    } );

                    throw new InvalidOperationException( $@"客戶端傳送訊息至伺服器失敗，詳細請參照 Inner Exception。
Inner Execption 訊息 : {e.Message}", e );
                }
                finally
                {
                    Close( client );
                    ResetDoneEvent( );
                }
            }
        }

        /// <summary>
        /// Returns a <see cref="System.String" /> that represents this instance.
        /// </summary>
        /// <returns>
        /// A <see cref="System.String" /> that represents this instance.
        /// </returns>
        public override string ToString( )
        {
            CheckIfDisposed( );
            return $"Client with {typeof( TCommandModel )} type model";
        }

        /// <inheritdoc />
        /// <summary>
        /// Releases unmanaged and - optionally - managed resources.
        /// </summary>
        /// <param name="disposing"><c>true</c> to release both managed and unmanaged resources; <c>false</c> to release only unmanaged resources.</param>
        protected override void Dispose( bool disposing )
        {
            lock ( _disposedLock )
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
        }

        /// <inheritdoc />
        /// <summary>
        /// Receives the command model callback.
        /// </summary>
        /// <param name="asyncResult">The asynchronous result.</param>
        protected override void ReceiveCommandModelCallback( IAsyncResult asyncResult )
        {
            base.ReceiveCommandModelCallback( asyncResult );

            if ( asyncResult.AsyncState is StateObjectModel<Socket> state )
            {
                var commandModel = GetCommandModel( state );
                InvokeOnSocketCommandModelReceived( commandModel );
            }

            _receiveDone.Set( );
        }

        /// <inheritdoc />
        /// <summary>
        /// Sends the commandModel with specified handler.
        /// </summary>
        /// <param name="handler">The handler.</param>
        /// <param name="commandModel">The command model.</param>
        /// <exception cref="T:System.InvalidOperationException"></exception>
        protected override void Send( Socket handler, TCommandModel commandModel )
        {
            try
            {
                base.Send( handler, commandModel );
            }
            catch ( Exception e )
            {
                throw new InvalidOperationException( $@"傳送資料失敗，詳細請參照 Inner Exception。
Inner Exception 訊息 : {e.Message}", e );
            }
        }

        /// <inheritdoc />
        /// <summary>
        /// Sends the callback.
        /// </summary>
        /// <param name="asyncResult">The asynchronous result.</param>
        protected override void SendCallback( IAsyncResult asyncResult )
        {
            base.SendCallback( asyncResult );
            _sendDone.Set( );
        }

        private static void CheckIfNullInput( TCommandModel commandModel )
        {
            if ( commandModel == null )
                throw new ArgumentNullException( nameof( commandModel ), "沒東西可以傳送喔，請記得初始化資料物件。" );
        }

        private static void CheckIfNullModel( ClientConfigModel clientConfigModel )
        {
            if ( clientConfigModel == null )
            {
                throw new ArgumentNullException( nameof( clientConfigModel ),
                    $@"時間 : {DateTime.Now.GetDateTimeString( )},
類別 : {nameof( Client<TCommandModel> )},
方法 : Constructor,
內容 : 你沒初始化 {nameof( clientConfigModel )} 喔。" );
            }
        }

        private static void ValidateModel( ClientConfigModel clientConfigModel )
        {
            if ( clientConfigModel.ValidateObject( out var errorMessages ) == false )
            {
                throw new ValidationException(
                    $@"時間 : {DateTime.Now.GetDateTimeString( )},
類別 : {nameof( Client<TCommandModel> )},
方法 : Constructor,
內容 : {string.Join( "\n", errorMessages )}" );
            }
        }

        private void CheckIfDisposed( )
        {
            if ( IsDisposed )
                throw new ObjectDisposedException( nameof( Client<TCommandModel> ), "Client 已經被 Dispose 囉" );
        }

        private void Close( Socket handler )
        {
            try
            {
                var remoteIp = handler.RemoteEndPoint;
                var localIp = handler.LocalEndPoint;

                handler.Shutdown( SocketShutdown.Both );
                handler.Close( );

                Logger?.Log( new LogModel
                {
                    Time = DateTime.Now,
                    Message = $"用戶端已關閉與伺服器連線, 伺服器 : {remoteIp}, 用戶端 : {localIp}"
                } );
            }
            catch ( Exception e )
            {
                Logger?.Log( new LogModel
                {
                    Time = DateTime.Now,
                    Message = $"用戶端關閉連線失敗囉, 物件名稱 : {ToString( )}, 例外訊息 : {e.Message}"
                } );
            }
            finally
            {
                SetDoneEvent( );
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
                        Time = DateTime.Now,
                        Message = $"用戶端已連線至伺服器, 伺服器 : {client.RemoteEndPoint}, 用戶端 : {client.LocalEndPoint}"
                    } );
                }
                catch ( Exception e )
                {
                    Logger?.Log( new LogModel
                    {
                        Time = DateTime.Now,
                        Message = $"客戶端連線伺服器失敗, 物件名稱 : {ToString( )}, 例外訊息 : {e.Message}"
                    } );
                }
                finally
                {
                    _connectDone.Set( );
                }
            }
        }

        private void Receive( Socket client )
        {
            try
            {
                var state = new StateObjectModel<Socket>
                {
                    WorkSocket = client,
                    TimeoutChecker = new TimeoutChecker<Socket>(
                        new TimeoutCheckerModel<Socket>
                        {
                            Target = client,
                            OnTimeoutAction = Close,
                            Logger = Logger,
                            TimeoutTime = TimeoutTime
                        } )
                };

                client.BeginReceive( state.Buffer, 0, StateObjectModel<Socket>.DataInfoSize, 0, ReadTotalLengthCallback, state );
            }
            catch ( Exception e )
            {
                Logger?.Log( new LogModel
                {
                    Time = DateTime.Now,
                    Message = $"資料接收失敗啦, 物件名稱 : {ToString( )}, 例外訊息 : {e.Message}"
                } );

                throw new InvalidOperationException( $@"客戶端接收伺服器訊息失敗，詳細請參照 Inner Exception。
Inner Execption 訊息 : {e.Message}", e );
            }
        }

        private void ResetDoneEvent( )
        {
            _connectDone.Reset( );
            _receiveDone.Reset( );
            _sendDone.Reset( );
        }

        private void SetDoneEvent( )
        {
            _connectDone.Set( );
            _receiveDone.Set( );
            _sendDone.Set( );
        }

        private void TrySend( Socket client, TCommandModel commandModel )
        {
            client.BeginConnect( RemoteIpEndPoint, ConnectCallback, client );
            _connectDone.Wait( );

            Send( client, commandModel );
            _sendDone.Wait( );

            Receive( client );
            _receiveDone.Wait( );
        }
    }
}