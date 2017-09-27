using HiLoSocket.Compressor;

namespace HiLoSocket.Builder.Client
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
        /// <returns>ISetTimeoutTime</returns>
        ISetTimeoutTime<T> SetCompressType( CompressType? compressType );
    }
}