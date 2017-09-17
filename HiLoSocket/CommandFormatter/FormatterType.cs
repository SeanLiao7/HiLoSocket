using System.ComponentModel;

namespace HiLoSocket.CommandFormatter
{
    public enum FormatterType
    {
        /// <summary>
        /// Default Formatter is BinaryFormatter
        /// </summary>
        [Description( "BinaryCommandFormatter" )]
        DefaultFormatter = 0,

        [Description( "JsonCommandFormatter" )]
        JSonFormatter = 1,

        [Description( "BinaryCommandFormatter" )]
        BinaryFormatter = 2,

        [Description( "MessagePackCommandFormatter" )]
        MessagePackFormatter = 3
    }
}