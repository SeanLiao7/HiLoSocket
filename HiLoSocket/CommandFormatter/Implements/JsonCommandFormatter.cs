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
                throw new ArgumentNullException( $"{nameof( bytes )}" );

            if ( bytes.Length == 0 )
                throw new ArgumentException( "陣列長度不能為零。", nameof( bytes ) );

            var str = Encoding.UTF8.GetString( bytes );
            return JsonConvert.DeserializeObject<T>( str );
        }

        public byte[ ] Serialize<T>( T command ) where T : class
        {
            if ( command == null )
                throw new ArgumentNullException( nameof( command ) );

            var jObject = JsonConvert.SerializeObject( command );
            return Encoding.UTF8.GetBytes( jObject );
        }
    }
}