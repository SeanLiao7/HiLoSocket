using System;
using System.Net.Sockets;
using HiLoSocket.CommandFormatter;
using HiLoSocket.Compressor;
using HiLoSocket.Logger;
using HiLoSocket.Model;
using HiLoSocket.Model.InternalOnly;

namespace HiLoSocket.SocketApp
{
    /// <inheritdoc />
    /// <summary>
    /// Base class for Socket app.
    /// </summary>
    /// <typeparam name="TCommandModel">The type of the command model.</typeparam>
    /// <seealso cref="T:System.IDisposable" />
    public abstract class SocketBase<TCommandModel> : IDisposable
        where TCommandModel : class
    {
        /// <summary>
        /// Gets the command formatter.
        /// </summary>
        /// <value>
        /// The command formatter.
        /// </value>
        protected ICommandFormatter<TCommandModel> CommandFormatter { get; }

        /// <summary>
        /// Gets the compressor.
        /// </summary>
        /// <value>
        /// The compressor.
        /// </value>
        protected ICompressor Compressor { get; }

        /// <summary>
        /// Gets a value indicating whether [ignore formatter].
        /// </summary>
        /// <value>
        ///   <c>true</c> if [ignore formatter]; otherwise, <c>false</c>.
        /// </value>
        protected bool IgnoreFormatter { get; }

        /// <summary>
        /// Gets the logger.
        /// </summary>
        /// <value>
        /// The logger.
        /// </value>
        protected ILogger Logger { get; }

        /// <summary>
        /// Occurs when [on command model received].
        /// </summary>
        public event Action<TCommandModel> OnCommandModelReceived;

        /// <summary>
        /// Initializes a new instance of the <see cref="SocketBase{TCommandModel}"/> class.
        /// </summary>
        /// <param name="formatterType">Type of the formatter.</param>
        /// <param name="compressType">Type of the compress.</param>
        /// <param name="logger">The logger.</param>
        internal SocketBase( FormatterType? formatterType, CompressType? compressType, ILogger logger )
        {
            IgnoreFormatter = typeof( TCommandModel ) == typeof( byte[ ] );

            if ( IgnoreFormatter == false )
            {
                if ( formatterType == null )
                    formatterType = FormatterType.BinaryFormatter;

                CommandFormatter = FormatterFactory<TCommandModel>.CreateFormatter( formatterType.Value );
            }

            if ( compressType == null )
                compressType = CompressType.Default;

            Compressor = CompressorFactory.CreateCompressor( compressType.Value );
            Logger = logger;
        }

        /// <inheritdoc />
        /// <summary>
        /// Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.
        /// </summary>
        public void Dispose( )
        {
            Dispose( true );
            GC.SuppressFinalize( this );
        }

        /// <summary>
        /// Gets the command model.
        /// </summary>
        /// <param name="state">The state.</param>
        /// <returns></returns>
        internal TCommandModel GetCommandModel( StateObjectModel<Socket> state )
        {
            var commandModel = default( TCommandModel );
            try
            {
                commandModel = IgnoreFormatter
                    ? state.Buffer as TCommandModel
                    : CommandFormatter.Deserialize( Compressor.Decompress( state.Buffer ) );
            }
            catch ( Exception e )
            {
                Logger?.Log( new LogModel
                {
                    Time = DateTime.Now,
                    Message = $"反序列化失敗囉, 物件名稱 : {ToString( )}, 例外訊息 : {e.Message}"
                } );
            }
            return commandModel;
        }

        /// <summary>
        /// Creates the size of the bytes to send with.
        /// </summary>
        /// <param name="commandBytestoSend">The command bytesto send.</param>
        /// <returns>Bytes to send with size.</returns>
        protected byte[ ] CreateBytesToSendWithSize( byte[ ] commandBytestoSend )
        {
            var lengthConvert = BitConverter.GetBytes( commandBytestoSend.Length ); // 將此次傳輸的 command 長度轉為 byte 陣列
            var commandBytetoSendWithSize = new byte[ commandBytestoSend.Length + StateObjectModel<Socket>.DataInfoSize ]; // 傳輸的資料包含長度包含 4 個代表 command 長度的陣列與本身
            lengthConvert.CopyTo( commandBytetoSendWithSize, 0 ); // copy 長度資訊
            commandBytestoSend.CopyTo( commandBytetoSendWithSize, StateObjectModel<Socket>.DataInfoSize ); // copy command 資訊

            return commandBytetoSendWithSize;
        }

        /// <summary>
        /// Releases unmanaged and - optionally - managed resources.
        /// </summary>
        /// <param name="disposing"><c>true</c> to release both managed and unmanaged resources; <c>false</c> to release only unmanaged resources.</param>
        protected virtual void Dispose( bool disposing )
        {
        }

        /// <summary>
        /// Invokes the on socket command model received.
        /// </summary>
        /// <param name="commandModel">The command model.</param>
        protected void InvokeOnSocketCommandModelReceived( TCommandModel commandModel )
        {
            OnCommandModelReceived?.Invoke( commandModel );
        }

        /// <summary>
        /// Reads the total length callback.
        /// </summary>
        /// <param name="asyncResult">The asynchronous result.</param>
        protected void ReadTotalLengthCallback( IAsyncResult asyncResult )
        {
            if ( asyncResult.AsyncState is StateObjectModel<Socket> state )
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
                        Time = DateTime.Now,
                        Message = $"資料長度訊息接收失敗, 物件名稱 : {ToString( )}, 例外訊息 : {e.Message}"
                    } );
                }

                if ( bytesRead == StateObjectModel<Socket>.DataInfoSize )
                {
                    Logger?.Log( new LogModel
                    {
                        Time = DateTime.Now,
                        Message = $"資料長度資訊已接收, 傳送端 : {handler.RemoteEndPoint}, 接收端 : {handler.LocalEndPoint}, 資料長度 : {bytesRead} bytes"
                    } );

                    TryReceiveCommandModel( handler, state );
                }
                else
                {
                    Logger?.Log( new LogModel
                    {
                        Time = DateTime.Now,
                        Message = $"資料長度訊息接收不完整喔, 物件名稱 : {ToString( )}, 資料長度 : {bytesRead} bytes"
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
            if ( asyncResult.AsyncState is StateObjectModel<Socket> state )
            {
                var bytesRead = default( int );
                var handler = state.WorkSocket;
                var timer = state.TimeoutChecker;

                try
                {
                    bytesRead = handler.EndReceive( asyncResult );
                    timer?.StopChecking( );
                }
                catch ( Exception e )
                {
                    Logger?.Log( new LogModel
                    {
                        Time = DateTime.Now,
                        Message = $"資料模型接收失敗, 物件名稱 : {ToString( )}, 例外訊息 : {e.Message}"
                    } );
                }

                if ( bytesRead == state.Buffer.Length )
                {
                    Logger?.Log( new LogModel
                    {
                        Time = DateTime.Now,
                        Message = $"資料模型已接收, 傳送端 : {handler.RemoteEndPoint}, 接收端 : {handler.LocalEndPoint}, 資料長度 : {bytesRead} bytes"
                    } );
                }
                else
                {
                    Logger?.Log( new LogModel
                    {
                        Time = DateTime.Now,
                        Message = $"資料模型收集不完全阿, 物件名稱 : {ToString( )}, 資料長度 : {bytesRead} bytes"
                    } );
                }
            }
        }

        /// <summary>
        /// Sends the commandModel with specified handler.
        /// </summary>
        /// <param name="handler">The handler.</param>
        /// <param name="commandModel">The command model.</param>
        protected virtual void Send( Socket handler, TCommandModel commandModel )
        {
            try
            {
                var bytestoSend = IgnoreFormatter
                    ? commandModel as byte[ ]
                    : CommandFormatter.Serialize( commandModel );

                var compressedbytes = Compressor.Compress( bytestoSend );
                var bytesToSendWithSize = CreateBytesToSendWithSize( compressedbytes );
                handler.BeginSend( bytesToSendWithSize, 0, bytesToSendWithSize.Length, 0, SendCallback, handler );
            }
            catch ( Exception e )
            {
                Logger?.Log( new LogModel
                {
                    Time = DateTime.Now,
                    Message = $"傳送資料失敗, 物件名稱 : {ToString( )}, 例外訊息 : {e.Message}"
                } );
                throw;
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
                        Time = DateTime.Now,
                        Message = $"資料已傳輸, 傳送端 : {handler.LocalEndPoint}, 接收端 : {handler.RemoteEndPoint}, 資料長度 : {bytesSent} bytes"
                    } );
                }
                catch ( Exception e )
                {
                    Logger?.Log( new LogModel
                    {
                        Time = DateTime.Now,
                        Message = $"資料傳輸失敗, 物件名稱 : {ToString( )}, 例外訊息 : {e.Message}"
                    } );
                }
            }
        }

        private void TryReceiveCommandModel( Socket handler, StateObjectModel<Socket> state )
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
                    Time = DateTime.Now,
                    Message = $"嘗試接收資料模型失敗, 物件名稱 : {ToString( )}, 例外訊息 : {e.Message}"
                } );
            }
        }
    }
}