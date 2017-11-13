using HiLoSocket.Compressor;
using NUnit.Framework;

namespace HiLoSocketTests.Compressor
{
    [TestFixture]
    [Category( "CompressorFactoryTests" )]
    public class CompressorFactoryTests
    {
        [TestCase( CompressType.Default )]
        [TestCase( CompressType.GZip )]
        [TestCase( CompressType.Deflate )]
        public void CreateCompressorTest( CompressType compressType )
        {
            Shouldly.Should.NotThrow(
                ( ) => CompressorFactory.CreateCompressor( compressType ) );
        }
    }
}