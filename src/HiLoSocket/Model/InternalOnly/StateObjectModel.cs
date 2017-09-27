using System.Net.Sockets;
using HiLoSocket.SocketApp;

namespace HiLoSocket.Model.InternalOnly
{
    /// <summary>
    /// StateObjectModel for socket async communication.
    /// </summary>
    /// <typeparam name="T">User define type.</typeparam>
    internal class StateObjectModel<T>
        where T : class
    {
        /// <summary>
        /// The data information size
        /// </summary>
        public const int DataInfoSize = 4;

        /// <summary>
        /// Gets or sets the buffer.
        /// </summary>
        /// <value>
        /// The buffer.
        /// </value>
        public byte[ ] Buffer { get; set; } = new byte[ DataInfoSize ];

        /// <summary>
        /// Gets or sets the timeout checker.
        /// </summary>
        /// <value>
        /// The timeout checker.
        /// </value>
        public TimeoutChecker<T> TimeoutChecker { get; set; }

        /// <summary>
        /// Gets or sets the work socket.
        /// </summary>
        /// <value>
        /// The work socket.
        /// </value>
        public Socket WorkSocket { get; set; }
    }
}