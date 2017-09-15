using HiLoSocket.AckCommand;
using HiLoSocket.SocketCommand;

namespace HiLoSocket
{
    public class SocketHandShake : ISocketHandShake
    {
        public AckCommandBase CreateAckCommand( SocketCommandBase socketCommand )
        {
            return new AckProgram( );
        }
    }
}