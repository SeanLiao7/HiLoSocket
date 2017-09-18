using System;
using MessagePack;
using MessagePack.Resolvers;

namespace HiLoSocket.CommandFormatter.Implements
{
    public class MessagePackCommandFormatter<TCommandModel> : ICommandFormatter<TCommandModel>
        where TCommandModel : class
    {
        /// <summary>
        /// Deserializes the specified bytes.
        /// </summary>
        /// <param name="bytes">The bytes.</param>
        /// <returns>
        /// TCommandModel.
        /// </returns>
        /// <exception cref="ArgumentNullException">bytes - 輸入參數沒東西可以反序列化喔。</exception>
        /// <exception cref="ArgumentException">資料長度不能為零阿。 - bytes</exception>
        public TCommandModel Deserialize( byte[ ] bytes )
        {
            if ( bytes == null )
                throw new ArgumentNullException( nameof( bytes ), "輸入參數沒東西可以反序列化喔。" );

            if ( bytes.Length == 0 )
                throw new ArgumentException( "資料長度不能為零阿。", nameof( bytes ) );

            return MessagePackSerializer.Deserialize<TCommandModel>( bytes, ContractlessStandardResolver.Instance );
        }

        /// <summary>
        /// Serializes the specified command model.
        /// </summary>
        /// <param name="commandModel">The command model.</param>
        /// <returns>
        /// Byte Array.
        /// </returns>
        /// <exception cref="ArgumentNullException">commandModel - 輸入參數沒東西可以序列化喔。</exception>
        public byte[ ] Serialize( TCommandModel commandModel )
        {
            if ( commandModel == null )
                throw new ArgumentNullException( nameof( commandModel ), "輸入參數沒東西可以序列化喔。" );

            var mPackObject = MessagePackSerializer.Serialize( commandModel, ContractlessStandardResolver.Instance );
            return mPackObject;
        }
    }
}