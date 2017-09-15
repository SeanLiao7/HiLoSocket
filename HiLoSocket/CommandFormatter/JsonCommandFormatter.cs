using System.Text;
using Newtonsoft.Json;

namespace HiLoSocket.CommandFormatter
{
    internal class JsonCommandFormatter : ICommandFormatter
    {
        public T Deserialize<T>( byte[ ] bytes ) where T : class
        {
            var str = Encoding.UTF8.GetString( bytes );
            return JsonConvert.DeserializeObject( str ) as T;
        }

        public byte[ ] Serialize<T>( T command ) where T : class
        {
            var jObject = JsonConvert.SerializeObject( command );
            return Encoding.UTF8.GetBytes( jObject );
        }
    }
}