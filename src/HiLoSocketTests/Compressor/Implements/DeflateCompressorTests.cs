using System;
using System.Linq;
using HiLoSocket.Compressor;
using NUnit.Framework;
using Shouldly;

namespace HiLoSocketTests.Compressor.Implements
{
    [TestFixture]
    public class DeflateCompressorTests
    {
        private readonly ICompressor _compressor =
            CompressorFactory.CreateCompressor( CompressType.Deflate );

        [TestCase( new byte[ ] { 255, 0, 147, 99, 88, 123, 200, 17, 189, 201 } )]
        public void CompressByteArrayTest( byte[ ] expected )
        {
            var compressed = _compressor.Compress( expected );
            var decompressed = _compressor.Decompress( compressed );
            Assert.IsTrue( decompressed.SequenceEqual( expected ) );
        }

        [TestCase( null )]
        public void CompressNullInputTest( byte[ ] input )
        {
            Should.Throw<ArgumentNullException>(
                ( ) => _compressor.Compress( input ) );
        }

        [TestCase( new byte[ 0 ] )]
        public void CompressZeroLengthInputTest( byte[ ] input )
        {
            Should.Throw<ArgumentException>(
                ( ) => _compressor.Compress( input ) );
        }

        [TestCase( null )]
        public void DecompressNullInputTest( byte[ ] input )
        {
            Should.Throw<ArgumentNullException>(
                ( ) => _compressor.Decompress( input ) );
        }

        [TestCase( new byte[ 0 ] )]
        public void DecompressZeroLengthInputTest( byte[ ] input )
        {
            Should.Throw<ArgumentException>(
                ( ) => _compressor.Decompress( input ) );
        }
    }
}