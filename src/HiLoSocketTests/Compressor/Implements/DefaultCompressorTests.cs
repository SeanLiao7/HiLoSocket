using System;
using System.Linq;
using HiLoSocket.Compressor;
using NUnit.Framework;
using Shouldly;

namespace HiLoSocketTests.Compressor.Implements
{
    [TestFixture]
    [Category( "DefaultCompressorTests" )]
    public class DefaultCompressorTests
    {
        private readonly ICompressor _compressor =
            CompressorFactory.CreateCompressor( CompressType.Default );

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
            var isSequentialEqual = decompressed.SequenceEqual( expected );
            isSequentialEqual.ShouldBe( true );
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