using System;
using HiLoSocket.CommandFormatter;

namespace HiLoSocket
{
    public abstract class SocketBase
    {
        internal ICommandFormatter CommandFormatter { get; }

        protected SocketBase( FormatterType formatterType )
        {
            CommandFormatter = FormatterFactory.CreateFormatter( formatterType );
        }

        protected byte[ ] CreateBytsToSendWithSize( byte[ ] commandBytetoSend )
        {
            var lengthConvert = BitConverter.GetBytes( commandBytetoSend.Length ); // 將此次傳輸的command 長度轉為byte陣列
            var commandBytetoSendWithSize = new byte[ commandBytetoSend.Length + 4 ]; // 傳輸的資料包含長度包含4個代表command長度的陣列與本身
            lengthConvert.CopyTo( commandBytetoSendWithSize, 0 ); // copy 長度資訊
            commandBytetoSend.CopyTo( commandBytetoSendWithSize, 4 ); // copy command 資訊

            return commandBytetoSendWithSize;
        }
    }
}