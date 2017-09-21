using System;
using HiLoSocket.Logger;
using HiLoSocket.Model;

namespace ClientForm
{
    internal class FormLogger : ILogger
    {
        public event Action<LogModel> OnLog;

        public void Log( LogModel logModel )
        {
            OnLog?.Invoke( logModel );
        }
    }
}