namespace HiLoSocket.Builder.Client
{
    /// <summary>
    /// ISetTimeoutTime.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface ISetTimeoutTime<T>
        where T : class
    {
        /// <summary>
        /// Sets the timeout time.
        /// </summary>
        /// <param name="timeoutTime">The timeout time.</param>
        /// <returns>ISetLogger.</returns>
        ISetLogger<T> SetTimeoutTime( int timeoutTime );
    }
}