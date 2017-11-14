using System.Collections;
using System.Collections.Generic;
using HiLoSocket.CommandFormatter;
using HiLoSocket.Compressor;
using NUnit.Framework;

namespace HiLoSocketTests.Compressor
{
    [TestFixture]
    [Category( "CompressorFactoryTests" )]
    public class CompressorFactoryTests
    {
        [TestCaseSource( typeof( CompressTypeSource ) )]
        public void CreateCompressor_CompressType_ShouldNotThrowException( CompressType compressType )
        {
            Shouldly.Should.NotThrow(
                ( ) => CompressorFactory.CreateCompressor( compressType ) );
        }

        private class CompressTypeSource : IEnumerable<CompressType>
        {
            public IEnumerator<CompressType> GetEnumerator( )
            {
                yield return CompressType.Default;
                yield return CompressType.GZip;
                yield return CompressType.Default;
            }

            IEnumerator IEnumerable.GetEnumerator( )
            {
                return GetEnumerator( );
            }
        }
    }
}