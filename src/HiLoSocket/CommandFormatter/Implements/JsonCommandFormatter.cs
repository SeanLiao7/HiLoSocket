using System;
using System.Text;
using Newtonsoft.Json;

namespace HiLoSocket.CommandFormatter.Implements
{
    internal sealed class JsonCommandFormatter<TCommandModel> : ICommandFormatter<TCommandModel>
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
        /// <exception cref="T:System.ArgumentNullException">bytes - 輸入參數沒東西可以反序列化喔</exception>
        /// <exception cref="T:System.ArgumentException">資料長度不能為零啦。 - bytes</exception>
        public TCommandModel Deserialize( byte[ ] bytes )
        {
            CheckIfCanBeDeserialized( bytes );
            var str = Encoding.UTF8.GetString( bytes );
            return JsonConvert.DeserializeObject<TCommandModel>( str );
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
            var jObject = JsonConvert.SerializeObject( commandModel );
            return Encoding.UTF8.GetBytes( jObject );
        }

        private static void CheckIfCanBeDeserialized( byte[ ] bytes )
        {
            if ( bytes == null )
                throw new ArgumentNullException( nameof( bytes ), "輸入參數沒東西可以反序列化喔" );

            if ( bytes.Length == 0 )
                throw new ArgumentException( "資料長度不能為零啦。", nameof( bytes ) );
        }

        private static void CheckIfCanBeSerialized( TCommandModel commandModel )
        {
            if ( commandModel == null )
                throw new ArgumentNullException( nameof( commandModel ), "輸入參數沒東西可以序列化喔。" );
        }
    }
}