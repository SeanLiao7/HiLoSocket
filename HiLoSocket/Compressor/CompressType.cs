using System.ComponentModel;

namespace HiLoSocket.Compressor
{
    public enum CompressType
    {
        [Description( "DefaultCompressor" )]
        Default = 0,

        [Description( "GZipCompressor" )]
        GZip = 1,
    }
}