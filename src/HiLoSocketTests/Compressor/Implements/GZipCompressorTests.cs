﻿using System;
using System.Linq;
using HiLoSocket.Compressor;
using NUnit.Framework;
using Shouldly;

namespace HiLoSocketTests.Compressor.Implements
{
    [TestFixture]
    public class GZipCompressorTests
    {
        [Test]
        [TestCase( new byte[ ] { 255, 0, 147, 99, 88, 123, 200, 17, 189, 201 } )]
        public void CompressByteArrayTest( byte[ ] expected )
        {
            var compressor = CompressorFactory.CreateCompressor( CompressType.GZip );
            var compressed = compressor.Compress( expected );
            var decompressed = compressor.Decompress( compressed );
            Assert.IsTrue( decompressed.SequenceEqual( expected ) );
        }

        [Test]
        [TestCase( null )]
        public void CompressNullInputTest( byte[ ] input )
        {
            var compressor = CompressorFactory.CreateCompressor( CompressType.GZip );
            Should.Throw<ArgumentNullException>(
                ( ) => compressor.Compress( input ) );
        }

        [Test]
        [TestCase( new byte[ 0 ] )]
        public void CompressZeroLengthInputTest( byte[ ] input )
        {
            var compressor = CompressorFactory.CreateCompressor( CompressType.GZip );
            Should.Throw<ArgumentException>(
                ( ) => compressor.Compress( input ) );
        }

        [Test]
        [TestCase( null )]
        public void DecompressNullInputTest( byte[ ] input )
        {
            var compressor = CompressorFactory.CreateCompressor( CompressType.GZip );
            Should.Throw<ArgumentNullException>(
                ( ) => compressor.Decompress( input ) );
        }

        [Test]
        [TestCase( new byte[ 0 ] )]
        public void DecompressZeroLengthInputTest( byte[ ] input )
        {
            var compressor = CompressorFactory.CreateCompressor( CompressType.GZip );
            Should.Throw<ArgumentException>(
                ( ) => compressor.Decompress( input ) );
        }
    }
}