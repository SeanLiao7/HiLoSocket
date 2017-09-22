using System;
using System.CodeDom;
using System.Net.Sockets;
using System.Threading;

namespace HiLoSocket.SocketApp
{
    public class TimeoutChecker<T> where T : class
    {
        private readonly Action<T> _onTimeoutAction;
        private readonly T _target;
        private readonly Timer _timer;

        public TimeoutChecker( Action<T> onTimeoutAction, T target, int timeoutTime )
        {
            _onTimeoutAction = onTimeoutAction;
            _target = target;
            _timer = new Timer( OnTimeout, null, timeoutTime, Timeout.Infinite );
        }

        public void StopChecking( )
        {
            _timer.Dispose( );
        }

        private void OnTimeout( object obj )
        {
            _onTimeoutAction.Invoke( _target );
        }
    }
}