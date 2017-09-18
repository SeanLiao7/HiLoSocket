using System.Net.Sockets;

namespace HiLoSocket.Model
{
    // State object for reading client data asynchronously
    public class StateObjectModel
    {
        // Size of receive buffer.
        public const int DataInfoSize = 4;

        // Receive buffer.
        public byte[ ] Buffer { get; set; } = new byte[ DataInfoSize ];

        // Client  socket.
        public Socket WorkSocket { get; set; }
    }
}