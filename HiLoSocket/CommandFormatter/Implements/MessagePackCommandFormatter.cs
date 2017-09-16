using System;
using MessagePack;
using MessagePack.Resolvers;

namespace HiLoSocket.CommandFormatter.Implements
{
    public class MessagePackCommandFormatter : ICommandFormatter
    {
        public T Deserialize<T>( byte[ ] bytes ) where T : class
        {
            if ( bytes == null )
                throw new ArgumentNullException( $"{nameof( bytes )}" );

            if ( bytes.Length == 0 )
                throw new ArgumentException( "陣列長度不能為零。", nameof( bytes ) );

            return MessagePackSerializer.Deserialize<T>( bytes, ContractlessStandardResolver.Instance );
        }

        public byte[ ] Serialize<T>( T command ) where T : class
        {
            if ( command == null )
                throw new ArgumentNullException( nameof( command ) );

            var mPackObject = MessagePackSerializer.Serialize( command, ContractlessStandardResolver.Instance );
            return mPackObject;
        }
    }
}