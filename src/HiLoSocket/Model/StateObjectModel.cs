using System.Net.Sockets;
using HiLoSocket.SocketApp;

namespace HiLoSocket.Model
{
    // State object for reading client data asynchronously
    public class StateObjectModel<T> where T : class
    {
        // Size of receive buffer.
        public const int DataInfoSize = 4;

        // Receive buffer.
        public byte[ ] Buffer { get; set; } = new byte[ DataInfoSize ];

        public TimeoutChecker<T> TimeoutChecker { get; set; }

        // Client  socket.
        public Socket WorkSocket { get; set; }
    }
}