using System;

namespace HiLoSocket.Compressor.Implements
{
    internal class DefaultCompressor : ICompressor
    {
        public byte[ ] Compress( byte[ ] bytes )
        {
            CheckIfCanBeCompressed( bytes );
            return bytes;
        }

        public byte[ ] Decompress( byte[ ] bytes )
        {
            CheckIfCanBeDecompressed( bytes );
            return bytes;
        }

        private static void CheckIfCanBeCompressed( byte[ ] bytes )
        {
            if ( bytes == null )
                throw new ArgumentNullException( nameof( bytes ),
                    $"請記得初始化壓縮參數喔，類別名稱 : {nameof( DefaultCompressor )}。" );

            if ( bytes.Length == 0 )
                throw new ArgumentException( $"壓縮資料長度不能為零阿，類別名稱 : {nameof( DefaultCompressor )}。",
                    nameof( bytes ) );
        }

        private static void CheckIfCanBeDecompressed( byte[ ] bytes )
        {
            if ( bytes == null )
                throw new ArgumentNullException( nameof( bytes ),
                    $"請記得初始化解壓縮參數喔，類別名稱 : {nameof( DefaultCompressor )}。" );

            if ( bytes.Length == 0 )
                throw new ArgumentException( $"解壓縮資料長度不能為零阿，類別名稱 : {nameof( DefaultCompressor )}。",
                    nameof( bytes ) );
        }
    }
}