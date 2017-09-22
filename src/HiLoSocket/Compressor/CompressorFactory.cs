using System;
using System.Collections.Generic;
using HiLoSocket.Extension;

namespace HiLoSocket.Compressor
{
    /// <summary>
    /// CompressorFactory.
    /// </summary>
    public static class CompressorFactory
    {
        private static readonly Dictionary<CompressType, ICompressor> _compressorTable =
            new Dictionary<CompressType, ICompressor>( );

        /// <summary>
        /// Creates the compressor.
        /// </summary>
        /// <param name="compressType">Type of the compress.</param>
        /// <returns></returns>
        /// <exception cref="InvalidOperationException">compressor</exception>
        public static ICompressor CreateCompressor( CompressType compressType )
        {
            if ( _compressorTable.TryGetValue( compressType, out var compressor ) )
                return compressor;

            var type = Type.GetType( $"HiLoSocket.Compressor.Implements.{compressType.GetDescription( )}" );
            if ( type != null )
            {
                compressor = Activator.CreateInstance( type ) as ICompressor;
                _compressorTable.Add( compressType, compressor );
            }

            if ( compressor == null )
                throw new InvalidOperationException( $"無法建立對應 {nameof( compressor )} 的物件。" );

            return compressor;
        }
    }
}