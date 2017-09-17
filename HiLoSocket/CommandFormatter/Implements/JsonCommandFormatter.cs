using System;
using System.Text;
using Newtonsoft.Json;

namespace HiLoSocket.CommandFormatter.Implements
{
    internal class JsonCommandFormatter : ICommandFormatter
    {
        public T Deserialize<T>( byte[ ] bytes ) where T : class
        {
            if ( bytes == null )
                throw new ArgumentNullException( nameof( bytes ), "輸入參數沒東西可以反序列化喔" );

            if ( bytes.Length == 0 )
                throw new ArgumentException( "資料長度不能為零啦。", nameof( bytes ) );

            var str = Encoding.UTF8.GetString( bytes );
            return JsonConvert.DeserializeObject<T>( str );
        }

        public byte[ ] Serialize<T>( T command ) where T : class
        {
            if ( command == null )
                throw new ArgumentNullException( nameof( command ), "輸入參數沒東西可以序列化喔。" );

            var jObject = JsonConvert.SerializeObject( command );
            return Encoding.UTF8.GetBytes( jObject );
        }
    }
}