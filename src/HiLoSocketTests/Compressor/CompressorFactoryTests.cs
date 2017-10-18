using HiLoSocket.Compressor;
using NUnit.Framework;

namespace HiLoSocketTests.Compressor
{
    [TestFixture]
    public class CompressorFactoryTests
    {
        [TestCase( CompressType.Default )]
        [TestCase( CompressType.GZip )]
        public void CreateCompressorTest( CompressType compressType )
        {
            Shouldly.Should.NotThrow(
                ( ) => CompressorFactory.CreateCompressor( compressType ) );
        }
    }
}