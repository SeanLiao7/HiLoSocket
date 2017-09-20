using System.ComponentModel;

namespace HiLoSocket.Compressor
{
    public enum CompressType
    {
        /// <summary>
        /// Default Compressor ( Non-Compressed )
        /// </summary>
        [Description( "DefaultCompressor" )]
        Default = 0,

        /// <summary>
        /// GZip Compressor
        /// </summary>
        [Description( "GZipCompressor" )]
        GZip = 1,
    }
}