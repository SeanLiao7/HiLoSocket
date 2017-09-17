using System;
using System.Net.Sockets;
using HiLoSocket.CommandFormatter;

namespace HiLoSocket
{
    public abstract class SocketBase<TModel> where TModel : class
    {
        protected ICommandFormatter CommandFormatter { get; }

        public event Action<TModel> OnSocketCommandModelRecieved;

        protected SocketBase( FormatterType? formatterType )
        {
            if ( formatterType == null )
                formatterType = FormatterType.BinaryFormatter;

            CommandFormatter = FormatterFactory.CreateFormatter( formatterType.Value );
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
                var handler = state.WorkSocket;
                var bytesRead = handler.EndReceive( asyncResult );

                if ( bytesRead == state.Buffer.Length )
                {
                    var model = CommandFormatter.Deserialize<TModel>( state.Buffer );
                    Send( handler, model );
                    OnSocketCommandModelRecieved?.Invoke( model );
                }
                else
                {
                    // TODO : log
                    throw new InvalidOperationException(
                        $@"時間 : {DateTime.Now.GetDateTimeString( )};
類別 : {nameof( SocketBase<TModel> )};
方法 : ReadModel;
內容 : 資料收集不完全阿。" );
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
                    var totalBufferSize = BitConverter.ToInt32( state.Buffer, 0 );
                    state.Buffer = new byte[ totalBufferSize ];
                    handler.BeginReceive( state.Buffer, 0, totalBufferSize, SocketFlags.None, ReadModel, state );
                }
                else
                {
                    // TODO : log
                    throw new InvalidOperationException(
                        $@"時間 : {DateTime.Now.GetDateTimeString( )};
類別 : {nameof( SocketBase<TModel> )};
方法 : ReadTotalLength;
內容 : 資料大小訊息收集失敗囉。" );
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
            var bytestoSend = CommandFormatter.Serialize( socketCommandModel );
            var bytesToSendWithSize = CreateBytesToSendWithSize( bytestoSend );
            handler.BeginSend( bytesToSendWithSize, 0, bytesToSendWithSize.Length, 0, SendCallback, handler );
        }

        /// <summary>
        /// Sends the callback.
        /// </summary>
        /// <param name="asyncResult">The asynchronous result.</param>
        protected virtual void SendCallback( IAsyncResult asyncResult )
        {
            try
            {
                if ( asyncResult.AsyncState is Socket handler )
                {
                    var bytesSent = handler.EndSend( asyncResult );
                    Console.WriteLine( $"Sent {bytesSent} bytes to client." );
                }
            }
            catch ( Exception e )
            {
                Console.WriteLine( e.ToString( ) );

                // TODO : log
                throw;
            }
        }
    }
}