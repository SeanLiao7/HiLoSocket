using System.Net.Sockets;

namespace HiLoSocket
{
    // State object for reading client data asynchronously
    public class StateObject
    {
        // Size of receive buffer.
        public const int DataInfoSize = 4;

        // Receive buffer.
        public byte[ ] Buffer { get; set; } = new byte[ DataInfoSize ];

        // Client  socket.
        public Socket WorkSocket { get; set; }
    }
}