using System;
using System.Collections;
using System.Linq;
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

        private class CompressTypeSource : IEnumerable
        {
            public IEnumerator GetEnumerator( )
            {
                return Enum.GetValues( typeof( CompressType ) ).Cast<CompressType>( ).GetEnumerator( );
            }
        }
    }
}