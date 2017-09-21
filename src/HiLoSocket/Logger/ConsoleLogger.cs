using System.Diagnostics;
using HiLoSocket.Model;

namespace HiLoSocket.Logger
{
    public class ConsoleLogger : ILogger
    {
        public void Log( LogModel logModel )
        {
            Trace.WriteLine( $"Time : {logModel.Time}, Message : {logModel.Message}" );
        }
    }
}