using System;
using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;

namespace HiLoSocket.CommandFormatter.Implements
{
    internal class BinaryCommandFormatter : ICommandFormatter
    {
        public T Deserialize<T>( byte[ ] bytes ) where T : class
        {
            if ( bytes == null )
                throw new ArgumentNullException( nameof( bytes ), "輸入參數沒東西可以反序列化喔。" );

            if ( bytes.Length == 0 )
                throw new ArgumentException( "資料長度不能為零阿。", nameof( bytes ) );

            if ( typeof( T ).IsSerializable == false )
                throw new SerializationException( "你忘記設定物件為可序列化囉。" );

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
            if ( command == null )
                throw new ArgumentNullException( nameof( command ), "輸入參數沒東西可以序列化喔。" );

            if ( command.GetType( ).IsSerializable == false )
                throw new SerializationException( "你忘記設定物件為可序列化囉。" );

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