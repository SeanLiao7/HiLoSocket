using System;
using System.IO;
using System.IO.Compression;

namespace HiLoSocket.Compressor.Implements
{
    internal class GZipCompressor : ICompressor
    {
        private const int BufferSize = 4096;

        public byte[ ] Compress( byte[ ] bytes )
        {
            CheckIfCanBeCompressed( bytes );
            byte[ ] compressed;
            using ( var memory = new MemoryStream( ) )
            {
                using ( var gzip = new GZipStream( memory, CompressionMode.Compress, true ) )
                {
                    gzip.Write( bytes, 0, bytes.Length );
                }
                compressed = memory.ToArray( );
            }

            return compressed;
        }

        public byte[ ] Decompress( byte[ ] bytes )
        {
            CheckIfCanBeDecompressed( bytes );
            byte[ ] decompressed;
            using ( var stream = new GZipStream( new MemoryStream( bytes ), CompressionMode.Decompress ) )
            {
                var buffer = new byte[ BufferSize ];
                using ( var memory = new MemoryStream( ) )
                {
                    var count = 0;
                    do
                    {
                        count = stream.Read( buffer, 0, BufferSize );
                        if ( count > 0 )
                        {
                            memory.Write( buffer, 0, count );
                        }
                    } while ( count > 0 );
                    decompressed = memory.ToArray( );
                }
            }

            return decompressed;
        }

        private static void CheckIfCanBeCompressed( byte[ ] bytes )
        {
            if ( bytes == null )
                throw new ArgumentNullException( nameof( bytes ),
                    $"請記得初始化壓縮參數喔，類別名稱 : {nameof( GZipCompressor )}。" );

            if ( bytes.Length == 0 )
                throw new ArgumentException( $"壓縮資料長度不能為零阿，類別名稱 : {nameof( GZipCompressor )}。",
                    nameof( bytes ) );
        }

        private static void CheckIfCanBeDecompressed( byte[ ] bytes )
        {
            if ( bytes == null )
                throw new ArgumentNullException( nameof( bytes ),
                    $"請記得初始化解壓縮參數喔，類別名稱 : {nameof( GZipCompressor )}。" );

            if ( bytes.Length == 0 )
                throw new ArgumentException( $"解壓縮資料長度不能為零阿，類別名稱 : {nameof( GZipCompressor )}。",
                    nameof( bytes ) );
        }
    }
}