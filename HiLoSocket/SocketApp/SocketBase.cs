using System;
using System.Net.Sockets;
using HiLoSocket.CommandFormatter;
using HiLoSocket.Logger;
using HiLoSocket.Model;

namespace HiLoSocket.SocketApp
{
    public abstract class SocketBase<TCommandModel> : IDisposable
        where TCommandModel : class
    {
        protected ICommandFormatter<TCommandModel> CommandFormatter { get; }

        protected ILogger Logger { get; }

        /// <summary>
        /// Occurs when [on command model recieved].
        /// </summary>
        public event Action<TCommandModel> OnCommandModelRecieved;

        protected SocketBase( FormatterType? formatterType, ILogger logger )
        {
            if ( formatterType == null )
                formatterType = FormatterType.BinaryFormatter;

            CommandFormatter = FormatterFactory<TCommandModel>.CreateFormatter( formatterType.Value );
            Logger = logger;
        }

        /// <summary>
        /// Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.
        /// </summary>
        public void Dispose( )
        {
            Dispose( true );
        }

        /// <summary>
        /// Creates the size of the bytes to send with.
        /// </summary>
        /// <param name="commandBytestoSend">The command bytesto send.</param>
        /// <returns>Bytes to send with size.</returns>
        protected byte[ ] CreateBytesToSendWithSize( byte[ ] commandBytestoSend )
        {
            var lengthConvert = BitConverter.GetBytes( commandBytestoSend.Length ); // 將此次傳輸的 command 長度轉為 byte 陣列
            var commandBytetoSendWithSize = new byte[ commandBytestoSend.Length + 4 ]; // 傳輸的資料包含長度包含 4 個代表 command 長度的陣列與本身
            lengthConvert.CopyTo( commandBytetoSendWithSize, 0 ); // copy 長度資訊
            commandBytestoSend.CopyTo( commandBytetoSendWithSize, 4 ); // copy command 資訊

            return commandBytetoSendWithSize;
        }

        /// <summary>
        /// Releases unmanaged and - optionally - managed resources.
        /// </summary>
        /// <param name="disposing"><c>true</c> to release both managed and unmanaged resources; <c>false</c> to release only unmanaged resources.</param>
        protected virtual void Dispose( bool disposing )
        {
        }

        protected TCommandModel GetCommandModel( StateObjectModel state )
        {
            TCommandModel commandModel = default( TCommandModel );
            try
            {
                commandModel = CommandFormatter.Deserialize( state.Buffer );
            }
            catch ( Exception e )
            {
                Logger?.Log( new LogModel
                {
                    LogTime = DateTime.Now,
                    LogMessage = $"反序列化失敗囉, 例外訊息 : {e.Message}"
                } );
            }
            return commandModel;
        }

        /// <summary>
        /// Invokes the on socket command model recieved.
        /// </summary>
        /// <param name="commandModel">The command model.</param>
        protected void InvokeOnSocketCommandModelRecieved( TCommandModel commandModel )
        {
            OnCommandModelRecieved?.Invoke( commandModel );
        }

        /// <summary>
        /// Reads the total length callback.
        /// </summary>
        /// <param name="asyncResult">The asynchronous result.</param>
        protected void ReadTotalLengthCallback( IAsyncResult asyncResult )
        {
            if ( asyncResult.AsyncState is StateObjectModel state )
            {
                var bytesRead = default( int );
                var handler = state.WorkSocket;

                try
                {
                    bytesRead = handler.EndReceive( asyncResult );
                }
                catch ( Exception e )
                {
                    Logger?.Log( new LogModel
                    {
                        LogTime = DateTime.Now,
                        LogMessage = $"資料長度訊息接收失敗, 例外訊息 : {e.Message}"
                    } );
                }

                if ( bytesRead == StateObjectModel.DataInfoSize )
                {
                    Logger?.Log( new LogModel
                    {
                        LogTime = DateTime.Now,
                        LogMessage = $"資料長度資訊已接收, 傳送端 : {handler.RemoteEndPoint}, 接收端 : {handler.LocalEndPoint}, 資料長度 : {bytesRead} bytes"
                    } );

                    TryReceiveCommandModel( handler, state );
                }
                else
                {
                    Logger?.Log( new LogModel
                    {
                        LogTime = DateTime.Now,
                        LogMessage = $"資料長度訊息不完整喔, 傳送端 : {handler.RemoteEndPoint}, 接收端 : {handler.LocalEndPoint}, 資料長度 : {bytesRead} bytes"
                    } );
                }
            }
        }

        /// <summary>
        /// Receives the command model callback.
        /// </summary>
        /// <param name="asyncResult">The asynchronous result.</param>
        protected virtual void ReceiveCommandModelCallback( IAsyncResult asyncResult )
        {
            if ( asyncResult.AsyncState is StateObjectModel state )
            {
                var bytesRead = default( int );
                var handler = state.WorkSocket;

                try
                {
                    bytesRead = handler.EndReceive( asyncResult );
                }
                catch ( Exception e )
                {
                    Logger?.Log( new LogModel
                    {
                        LogTime = DateTime.Now,
                        LogMessage = $"資料模型接收失敗, 例外訊息 : {e.Message}"
                    } );
                }

                if ( bytesRead == state.Buffer.Length )
                {
                    Logger?.Log( new LogModel
                    {
                        LogTime = DateTime.Now,
                        LogMessage = $"資料模型已接收, 傳送端 : {handler.RemoteEndPoint}, 接收端 : {handler.LocalEndPoint}, 資料長度 : {bytesRead} bytes"
                    } );
                }
                else
                {
                    Logger?.Log( new LogModel
                    {
                        LogTime = DateTime.Now,
                        LogMessage = $"資料模型收集不完全阿, 傳送端 : {handler.RemoteEndPoint}, 接收端 : {handler.LocalEndPoint}, 資料長度 : {bytesRead} bytes"
                    } );
                }
            }
        }

        /// <summary>
        /// Sends the specified handler.
        /// </summary>
        /// <param name="handler">The handler.</param>
        /// <param name="commandModel">The command model.</param>
        /// <exception cref="InvalidOperationException">傳送資料失敗，詳細請參照 Inner Exception。</exception>
        protected void Send( Socket handler, TCommandModel commandModel )
        {
            try
            {
                var bytestoSend = CommandFormatter.Serialize( commandModel );
                var bytesToSendWithSize = CreateBytesToSendWithSize( bytestoSend );
                handler.BeginSend( bytesToSendWithSize, 0, bytesToSendWithSize.Length, 0, SendCallback, handler );
            }
            catch ( Exception e )
            {
                Logger?.Log( new LogModel
                {
                    LogTime = DateTime.Now,
                    LogMessage = $"傳送資料失敗, 例外訊息 : {e.Message}"
                } );

                throw new InvalidOperationException( $@"傳送資料失敗，詳細請參照 Inner Exception。
Inner Exception 訊息 : {e.Message}", e );
            }
        }

        /// <summary>
        /// Sends the callback.
        /// </summary>
        /// <param name="asyncResult">The asynchronous result.</param>
        protected virtual void SendCallback( IAsyncResult asyncResult )
        {
            if ( asyncResult.AsyncState is Socket handler )
            {
                try
                {
                    var bytesSent = handler.EndSend( asyncResult );
                    Logger?.Log( new LogModel
                    {
                        LogTime = DateTime.Now,
                        LogMessage = $"資料已傳輸, 傳送端 : {handler.LocalEndPoint}, 接收端 : {handler.RemoteEndPoint}, 資料長度 : {bytesSent} bytes"
                    } );
                }
                catch ( Exception e )
                {
                    Logger?.Log( new LogModel
                    {
                        LogTime = DateTime.Now,
                        LogMessage = $"資料傳輸失敗, 例外訊息 : {e.Message}"
                    } );
                }
            }
        }

        private void TryReceiveCommandModel( Socket handler, StateObjectModel state )
        {
            var totalBufferSize = BitConverter.ToInt32( state.Buffer, 0 );
            state.Buffer = new byte[ totalBufferSize ];
            try
            {
                handler.BeginReceive( state.Buffer, 0, totalBufferSize, SocketFlags.None, ReceiveCommandModelCallback, state );
            }
            catch ( Exception e )
            {
                Logger?.Log( new LogModel
                {
                    LogTime = DateTime.Now,
                    LogMessage = $"嘗試接收資料模型失敗, 例外訊息 : {e.Message}"
                } );
            }
        }
    }
}