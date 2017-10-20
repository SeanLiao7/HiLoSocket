using System.ComponentModel;

namespace HiLoSocket.Compressor
{
    /// <summary>
    /// CompressType.
    /// </summary>
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

        /// <summary>
        /// Deflate Compressor
        /// </summary>
        [Description( "DeflateCompressor" )]
        Deflate = 2,
    }
}