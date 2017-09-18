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

        /// <summary>
        /// Json Formatter
        /// </summary>
        [Description( "JsonCommandFormatter" )]
        JSonFormatter = 1,

        /// <summary>
        /// Binary Formatter
        /// </summary>
        [Description( "BinaryCommandFormatter" )]
        BinaryFormatter = 2,

        /// <summary>
        /// MessagePack Formatter
        /// </summary>
        [Description( "MessagePackCommandFormatter" )]
        MessagePackFormatter = 3
    }
}