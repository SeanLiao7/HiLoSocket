using HiLoSocket.CommandFormatter;

namespace HiLoSocket.Builder.Server
{
    /// <summary>
    /// ISetFormatterType.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface ISetFormatterType<T>
        where T : class
    {
        /// <summary>
        /// Sets the type of the formatter.
        /// </summary>
        /// <param name="formatterType">Type of the formatter.</param>
        /// <returns>ISetCompressType.</returns>
        ISetCompressType<T> SetFormatterType( FormatterType? formatterType );
    }
}