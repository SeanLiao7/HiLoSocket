using System;
using MessagePack;
using MessagePack.Resolvers;

namespace HiLoSocket.CommandFormatter.Implements
{
    internal class MessagePackCommandFormatter<TCommandModel> : ICommandFormatter<TCommandModel>
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
        public TCommandModel Deserialize( byte[ ] bytes )
        {
            CheckIfCanBeDeserialized( bytes );
            return MessagePackSerializer.Deserialize<TCommandModel>( bytes, ContractlessStandardResolver.Instance );
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
        public byte[ ] Serialize( TCommandModel commandModel )
        {
            CheckIfCanBeSerialized( commandModel );
            var mPackObject = MessagePackSerializer.Serialize( commandModel, ContractlessStandardResolver.Instance );
            return mPackObject;
        }

        private static void CheckIfCanBeDeserialized( byte[ ] bytes )
        {
            if ( bytes == null )
                throw new ArgumentNullException( nameof( bytes ), "輸入參數沒東西可以反序列化喔。" );

            if ( bytes.Length == 0 )
                throw new ArgumentException( "資料長度不能為零阿。", nameof( bytes ) );
        }

        private static void CheckIfCanBeSerialized( TCommandModel commandModel )
        {
            if ( commandModel == null )
                throw new ArgumentNullException( nameof( commandModel ), "輸入參數沒東西可以序列化喔。" );
        }
    }
}