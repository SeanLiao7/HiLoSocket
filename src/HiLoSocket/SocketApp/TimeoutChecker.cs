using System;
using System.Threading;

namespace HiLoSocket.SocketApp
{
    /// <summary>
    /// Timeout Checker.
    /// </summary>
    /// <typeparam name="T">User define type.</typeparam>
    public class TimeoutChecker<T> where T : class
    {
        private readonly Action<T> _onTimeoutAction;
        private readonly T _target;
        private readonly Timer _timer;

        /// <summary>
        /// Initializes a new instance of the <see cref="TimeoutChecker{T}"/> class.
        /// </summary>
        /// <param name="onTimeoutAction">The on timeout action.</param>
        /// <param name="target">The target.</param>
        /// <param name="timeoutTime">The timeout time.</param>
        public TimeoutChecker( Action<T> onTimeoutAction, T target, int timeoutTime )
        {
            _onTimeoutAction = onTimeoutAction;
            _target = target;
            _timer = new Timer( OnTimeout, null, timeoutTime, Timeout.Infinite );
        }

        /// <summary>
        /// Stops checking.
        /// </summary>
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