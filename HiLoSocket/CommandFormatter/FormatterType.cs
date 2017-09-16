using System.ComponentModel;

namespace HiLoSocket.CommandFormatter
{
    public enum FormatterType
    {
        [Description( "JsonCommandFormatter" )]
        JSonFormatter = 0x01,

        [Description( "BinaryCommandFormatter" )]
        BinaryFormatter = 0x02,

        [Description( "MessagePackCommandFormatter" )]
        MessagePackFormatter = 0x04
    }
}