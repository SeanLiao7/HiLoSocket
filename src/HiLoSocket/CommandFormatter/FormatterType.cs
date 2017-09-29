using System.ComponentModel;

namespace HiLoSocket.CommandFormatter
{
    /// <summary>
    /// FormatterType.
    /// </summary>
    public enum FormatterType
    {
        /// <summary>
        /// Binary Formatter
        /// </summary>
        [Description( "BinaryCommandFormatter" )]
        BinaryFormatter = 0,

        /// <summary>
        /// Json Formatter
        /// </summary>
        [Description( "JsonCommandFormatter" )]
        JSonFormatter = 1,

        /// <summary>
        /// MessagePack Formatter
        /// </summary>
        //[Description( "MessagePackCommandFormatter" )]
        //MessagePackFormatter = 2
    }
}