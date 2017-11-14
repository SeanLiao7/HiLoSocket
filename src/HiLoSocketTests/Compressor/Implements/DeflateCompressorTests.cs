using System;
using System.Linq;
using HiLoSocket.Compressor;
using NUnit.Framework;
using Shouldly;

namespace HiLoSocketTests.Compressor.Implements
{
    [TestFixture]
    [Category( "DeflateCompressorTests" )]
    public class DeflateCompressorTests
    {
        private readonly ICompressor _compressor =
            CompressorFactory.CreateCompressor( CompressType.Deflate );

        [TestCase( null )]
        public void Compress_NullInput_ThrowsArgumentNullException( byte[ ] input )
        {
            Should.Throw<ArgumentNullException>(
                ( ) => _compressor.Compress( input ) );
        }

        [TestCase( new byte[ 0 ] )]
        public void Compress_ZeroLengthInput_ThrowsArgumentException( byte[ ] input )
        {
            Should.Throw<ArgumentException>(
                ( ) => _compressor.Compress( input ) );
        }

        [TestCase( new byte[ ] { 255, 0, 147, 99, 88, 123, 200, 17, 189, 201 } )]
        public void CompressAndDecompress_ByteArray_ShouldBeSequenceEqual( byte[ ] expected )
        {
            var compressed = _compressor.Compress( expected );
            var decompressed = _compressor.Decompress( compressed );
            Assert.That( decompressed, Is.EquivalentTo( expected ) );
        }

        [TestCase( null )]
        public void Decompress_NullInput_ThrowsArgumentNullException( byte[ ] input )
        {
            Should.Throw<ArgumentNullException>(
                ( ) => _compressor.Decompress( input ) );
        }

        [TestCase( new byte[ 0 ] )]
        public void Decompress_ZeroLengthInput_ThrowsArgumentException( byte[ ] input )
        {
            Should.Throw<ArgumentException>(
                ( ) => _compressor.Decompress( input ) );
        }
    }
}