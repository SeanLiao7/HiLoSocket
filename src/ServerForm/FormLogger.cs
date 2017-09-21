using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HiLoSocket.Logger;
using HiLoSocket.Model;

namespace ServerForm
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