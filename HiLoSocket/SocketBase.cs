using System;
using System.Diagnostics;
using System.Net.Sockets;
using HiLoSocket.CommandFormatter;
using HiLoSocket.Logger;
using HiLoSocket.Model;

namespace HiLoSocket
{
    public abstract class SocketBase<TModel> : IDisposable
        where TModel : class
    {
        protected ICommandFormatter CommandFormatter { get; }

        protected ILogger Logger { get; }

        public event Action<TModel> OnSocketCommandModelRecieved;

        protected SocketBase( FormatterType? formatterType, ILogger logger )
        {
            if ( formatterType == null )
                formatterType = FormatterType.BinaryFormatter;

            CommandFormatter = FormatterFactory.CreateFormatter( formatterType.Value );
            Logger = logger;
        }

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
            var lengthConvert = BitConverter.GetBytes( commandBytestoSend.Length ); // 將此次傳輸的command 長度轉為byte陣列
            var commandBytetoSendWithSize = new byte[ commandBytestoSend.Length + 4 ]; // 傳輸的資料包含長度包含4個代表command長度的陣列與本身
            lengthConvert.CopyTo( commandBytetoSendWithSize, 0 ); // copy 長度資訊
            commandBytestoSend.CopyTo( commandBytetoSendWithSize, 4 ); // copy command 資訊

            return commandBytetoSendWithSize;
        }

        protected virtual void Dispose( bool disposing )
        {
        }

        protected void InvokeOnSocketCommandModelRecieved( TModel model )
        {
            OnSocketCommandModelRecieved?.Invoke( model );
        }

        /// <summary>
        /// Reads the socket command model.
        /// </summary>
        /// <param name="asyncResult">The asynchronous result.</param>
        /// <param name="model">The model.</param>
        /// <returns>Successful or not</returns>
        protected virtual void ReadModel( IAsyncResult asyncResult )
        {
            if ( asyncResult.AsyncState is StateObject state )
            {
                var bytesRead = default( int );
                var handler = state.WorkSocket;

                try
                {
                    handler = state.WorkSocket;
                    bytesRead = handler.EndReceive( asyncResult );
                }
                catch ( Exception e )
                {
                    Logger?.Log( new LogModel
                    {
                        LogTime = DateTime.Now,
                        LogMessage = $"資料模型接收失敗, 傳送端 : {handler.RemoteEndPoint}, 接收端 : {handler.LocalEndPoint}, 例外訊息 : {e.Message}"
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
                        LogMessage = $"資料收集不完全阿, 傳送端 : {handler.RemoteEndPoint}, 接收端 : {handler.LocalEndPoint}, 資料長度 : {bytesRead} bytes"
                    } );
                }
            }
        }

        /// <summary>
        /// Reads the total length.
        /// </summary>
        /// <param name="asyncResult">The asynchronous result.</param>
        /// <exception cref="InvalidOperationException">資料大小訊息收集失敗囉。</exception>
        protected void ReadTotalLength( IAsyncResult asyncResult )
        {
            if ( asyncResult.AsyncState is StateObject state )
            {
                var handler = state.WorkSocket;
                var bytesRead = handler.EndReceive( asyncResult );

                if ( bytesRead == StateObject.DataInfoSize )
                {
                    Logger?.Log( new LogModel
                    {
                        LogTime = DateTime.Now,
                        LogMessage = $"資料長度資訊已接收, 傳送端 : {handler.RemoteEndPoint}, 接收端 : {handler.LocalEndPoint}, 資料長度 : {bytesRead} bytes"
                    } );

                    var totalBufferSize = BitConverter.ToInt32( state.Buffer, 0 );
                    state.Buffer = new byte[ totalBufferSize ];
                    handler.BeginReceive( state.Buffer, 0, totalBufferSize, SocketFlags.None, ReadModel, state );
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
        /// Sends the specified handler.
        /// </summary>
        /// <param name="handler">The handler.</param>
        /// <param name="socketCommandModel">The socket command model.</param>
        protected void Send( Socket handler, TModel socketCommandModel )
        {
            try
            {
                var bytestoSend = CommandFormatter.Serialize( socketCommandModel );
                var bytesToSendWithSize = CreateBytesToSendWithSize( bytestoSend );
                handler.BeginSend( bytesToSendWithSize, 0, bytesToSendWithSize.Length, 0, SendCallback, handler );
            }
            catch ( Exception e )
            {
                Logger?.Log( new LogModel
                {
                    LogTime = DateTime.Now,
                    LogMessage = $"傳送資料失敗, 傳送端 : {handler.LocalEndPoint}, 接收端 : {handler.RemoteEndPoint}, 例外訊息 : {e.Message}"
                } );

                throw new InvalidOperationException( "傳送資料失敗，詳細請參照 Inner Exception。", e );
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
                        LogMessage = $"資料傳輸失敗, 傳送端 : {handler.LocalEndPoint}, 接收端 : {handler.RemoteEndPoint}, 例外訊息 : {e.Message}"
                    } );
                }
            }
        }
    }
}