using HiLoSocket.Compressor;

namespace HiLoSocket.Builder.Server
{
    /// <summary>
    /// ISetCompressType.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface ISetCompressType<T>
        where T : class
    {
        /// <summary>
        /// Sets the type of the compress.
        /// </summary>
        /// <param name="compressType">Type of the compress.</param>
        /// <returns>ISetLogger.</returns>
        ISetLogger<T> SetCompressType( CompressType? compressType );
    }
}