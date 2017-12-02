using System;
using FluentAssertions;
using HiLoSocket.Compressor;
using NUnit.Framework;
using Ploeh.AutoFixture;
using Shouldly;

namespace HiLoSocketTests.Compressor.Implements
{
    [TestFixture]
    [Category( "DefaultCompressorTests" )]
    public class DefaultCompressorTests
    {
        private readonly ICompressor _compressor =
            CompressorFactory.CreateCompressor( CompressType.Default );

        [Test]
        public void Compress_NullInput_ThrowsArgumentNullException( )
        {
            var fixture = new Fixture( );
            fixture.Register<byte[ ]>( ( ) => null );
            var input = fixture.Create<byte[ ]>( );
            Should.Throw<ArgumentNullException>(
                ( ) => _compressor.Compress( input ) );
        }

        [Test]
        public void Compress_ZeroLengthInput_ThrowsArgumentException( )
        {
            var fixture = new Fixture
            {
                RepeatCount = 0
            };
            var input = fixture.Create<byte[ ]>( );
            Should.Throw<ArgumentException>(
                ( ) => _compressor.Compress( input ) );
        }

        [Test]
        public void CompressAndDecompress_ByteArray_ShouldBeSequenceEqual( )
        {
            var fixture = new Fixture
            {
                RepeatCount = 20
            };
            var expected = fixture.Create<byte[ ]>( );
            var compressed = _compressor.Compress( expected );
            var decompressed = _compressor.Decompress( compressed );
            decompressed.ShouldAllBeEquivalentTo( expected );
        }

        [Test]
        public void Decompress_NullInput_ThrowsArgumentNullException( )
        {
            var fixture = new Fixture( );
            fixture.Register<byte[ ]>( ( ) => null );
            var input = fixture.Create<byte[ ]>( );
            Should.Throw<ArgumentNullException>(
                ( ) => _compressor.Decompress( input ) );
        }

        [Test]
        public void Decompress_ZeroLengthInput_ThrowsArgumentException( )
        {
            var fixture = new Fixture
            {
                RepeatCount = 0
            };
            var input = fixture.Create<byte[ ]>( );
            Should.Throw<ArgumentException>(
                ( ) => _compressor.Decompress( input ) );
        }
    }
}