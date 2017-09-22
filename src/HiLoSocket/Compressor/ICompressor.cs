namespace HiLoSocket.Compressor
{
    /// <summary>
    /// ICompressor.
    /// </summary>
    public interface ICompressor
    {
        /// <summary>
        /// Compresses the specified bytes.
        /// </summary>
        /// <param name="bytes">The bytes.</param>
        /// <returns>Bytes</returns>
        byte[ ] Compress( byte[ ] bytes );

        /// <summary>
        /// Decompresses the specified bytes.
        /// </summary>
        /// <param name="bytes">The bytes.</param>
        /// <returns>Bytes</returns>
        byte[ ] Decompress( byte[ ] bytes );
    }
}