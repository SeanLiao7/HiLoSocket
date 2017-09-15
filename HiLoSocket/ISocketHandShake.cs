using HiLoSocket.AckCommand;
using HiLoSocket.SocketCommand;

namespace HiLoSocket
{
    public interface ISocketHandShake
    {
        AckCommandBase CreateAckCommand( SocketCommandBase socketCommandBase );
    }
}