using System;
using System.Text;
using Newtonsoft.Json;

namespace HiLoSocket.CommandFormatter.Implements
{
    internal class JsonCommandFormatter<TCommandModel> : ICommandFormatter<TCommandModel>
        where TCommandModel : class
    {
        /// <summary>
        /// Deserializes the specified bytes.
        /// </summary>
        /// <param name="bytes">The bytes.</param>
        /// <returns>
        /// TCommandModel.
        /// </returns>
        /// <exception cref="ArgumentNullException">bytes - 輸入參數沒東西可以反序列化喔</exception>
        /// <exception cref="ArgumentException">資料長度不能為零啦。 - bytes</exception>
        public TCommandModel Deserialize( byte[ ] bytes )
        {
            if ( bytes == null )
                throw new ArgumentNullException( nameof( bytes ), "輸入參數沒東西可以反序列化喔" );

            if ( bytes.Length == 0 )
                throw new ArgumentException( "資料長度不能為零啦。", nameof( bytes ) );

            var str = Encoding.UTF8.GetString( bytes );
            return JsonConvert.DeserializeObject<TCommandModel>( str );
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

            var jObject = JsonConvert.SerializeObject( commandModel );
            return Encoding.UTF8.GetBytes( jObject );
        }
    }
}