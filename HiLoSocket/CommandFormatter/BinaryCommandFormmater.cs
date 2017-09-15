using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

namespace HiLoSocket.CommandFormatter
{
    internal class BinaryCommandFormmater : ICommandFormatter
    {
        public T Deserialize<T>( byte[ ] bytes ) where T : class
        {
            T command;

            using ( var deserializeStream = new MemoryStream( bytes ) )
            {
                deserializeStream.Position = 0;
                var formatter = new BinaryFormatter( );
                command = formatter.Deserialize( deserializeStream ) as T;
            }

            return command;
        }

        public byte[ ] Serialize<T>( T command ) where T : class
        {
            byte[ ] commandBytetoSend;

            using ( var serializeStream = new MemoryStream( ) )
            {
                var formatter = new BinaryFormatter( );
                formatter.Serialize( serializeStream, command );
                commandBytetoSend = serializeStream.ToArray( );
            }

            return commandBytetoSend;
        }
    }
}