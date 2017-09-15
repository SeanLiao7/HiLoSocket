using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace HiLoSocket.CommandFormatter
{
    internal interface ICommandFormatter
    {
        T Deserialize<T>( byte[ ] bytes ) where T : class;

        byte[ ] Serialize<T>( T command ) where T : class;
    }
}