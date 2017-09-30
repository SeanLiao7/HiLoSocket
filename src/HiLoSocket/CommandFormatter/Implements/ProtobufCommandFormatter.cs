using System;
using System.IO;
using ProtoBuf;

namespace HiLoSocket.CommandFormatter.Implements
{
    internal sealed class ProtobufCommandFormatter<TCommandModel> : ICommandFormatter<TCommandModel>
        where TCommandModel : class
    {
        /// <inheritdoc />
        /// <summary>
        /// Deserializes the specified bytes.
        /// </summary>
        /// <param name="bytes">The bytes.</param>
        /// <returns>
        /// TCommandModel.
        /// </returns>
        public TCommandModel Deserialize( byte[ ] bytes )
        {
            CheckIfCanBeDeserialized( bytes );
            TCommandModel command;
            using ( var deserializeStream = new MemoryStream( bytes ) )
                command = Serializer.Deserialize<TCommandModel>( deserializeStream );

            return command;
        }

        /// <inheritdoc />
        /// <summary>
        /// Serializes the specified command model.
        /// </summary>
        /// <param name="commandModel">The command model.</param>
        /// <returns>
        /// Byte Array.
        /// </returns>
        public byte[ ] Serialize( TCommandModel commandModel )
        {
            CheckIfCanBeSerialized( commandModel );
            byte[ ] commandBytetoSend;
            using ( var serializeStream = new MemoryStream( ) )
            {
                Serializer.Serialize( serializeStream, commandModel );
                commandBytetoSend = serializeStream.ToArray( );
            }

            return commandBytetoSend;
        }

        private static void CheckIfCanBeDeserialized( byte[ ] bytes )
        {
            if ( bytes == null )
                throw new ArgumentNullException( nameof( bytes ),
                    $"輸入參數沒東西可以反序列化喔，類別名稱 : {nameof( ProtobufCommandFormatter<TCommandModel> )}。" );

            if ( bytes.Length == 0 )
                throw new ArgumentException(
                    $"資料長度不能為零啦，類別名稱 : {nameof( ProtobufCommandFormatter<TCommandModel> )}。",
                    nameof( bytes ) );
        }

        private static void CheckIfCanBeSerialized( TCommandModel commandModel )
        {
            if ( commandModel == null )
                throw new ArgumentNullException( nameof( commandModel ),
                    $"輸入參數沒東西可以序列化喔，類別名稱 : {nameof( ProtobufCommandFormatter<TCommandModel> )}。" );
        }
    }
}