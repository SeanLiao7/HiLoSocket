using System;
using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;

namespace HiLoSocket.CommandFormatter.Implements
{
    internal class BinaryCommandFormatter<TCommandModel> : ICommandFormatter<TCommandModel>
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
        /// <exception cref="T:System.ArgumentNullException">bytes - 輸入參數沒東西可以反序列化喔。</exception>
        /// <exception cref="T:System.ArgumentException">資料長度不能為零阿。 - bytes</exception>
        /// <exception cref="T:System.Runtime.Serialization.SerializationException">你忘記設定物件為可序列化囉。</exception>
        public TCommandModel Deserialize( byte[ ] bytes )
        {
            CheckIfCanBeDeserialized( bytes );
            TCommandModel command;
            using ( var deserializeStream = new MemoryStream( bytes ) )
            {
                deserializeStream.Position = 0;
                var formatter = new BinaryFormatter( );
                command = formatter.Deserialize( deserializeStream ) as TCommandModel;
            }

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
        /// <exception cref="T:System.ArgumentNullException">commandModel - 輸入參數沒東西可以序列化喔。</exception>
        /// <exception cref="T:System.Runtime.Serialization.SerializationException">你忘記設定物件為可序列化囉。</exception>
        public byte[ ] Serialize( TCommandModel commandModel )
        {
            CheckIfCanBeSerialized( commandModel );
            byte[ ] commandBytetoSend;
            using ( var serializeStream = new MemoryStream( ) )
            {
                var formatter = new BinaryFormatter( );
                formatter.Serialize( serializeStream, commandModel );
                commandBytetoSend = serializeStream.ToArray( );
            }

            return commandBytetoSend;
        }

        private static void CheckIfCanBeDeserialized( byte[ ] bytes )
        {
            if ( bytes == null )
                throw new ArgumentNullException( nameof( bytes ), "輸入參數沒東西可以反序列化喔。" );

            if ( bytes.Length == 0 )
                throw new ArgumentException( "資料長度不能為零阿。", nameof( bytes ) );

            if ( typeof( TCommandModel ).IsSerializable == false )
                throw new SerializationException( "你忘記設定物件為可序列化囉。" );
        }

        private static void CheckIfCanBeSerialized( TCommandModel commandModel )
        {
            if ( commandModel == null )
                throw new ArgumentNullException( nameof( commandModel ), "輸入參數沒東西可以序列化喔。" );

            if ( commandModel.GetType( ).IsSerializable == false )
                throw new SerializationException( "你忘記設定物件為可序列化囉。" );
        }
    }
}