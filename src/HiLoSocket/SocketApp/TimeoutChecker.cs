using System;
using System.Threading;
using HiLoSocket.Logger;
using HiLoSocket.Model;
using HiLoSocket.Model.InternalOnly;

namespace HiLoSocket.SocketApp
{
    /// <summary>
    /// Timeout Checker.
    /// </summary>
    /// <typeparam name="T">User define type.</typeparam>
    internal sealed class TimeoutChecker<T>
        where T : class
    {
        private readonly ILogger _logger;
        private readonly Action<T> _onTimeoutAction;
        private readonly int _timeoutTime;
        private readonly Timer _timer;

        /// <summary>
        /// Initializes a new instance of the <see cref="TimeoutChecker{T}"/> class.
        /// </summary>
        /// <param name="timeoutCheckerModel">The timeout checker model.</param>
        /// <exception cref="ArgumentNullException">timeoutCheckerModel - 建構子參數不能為空值喔，請記得初始化。</exception>
        internal TimeoutChecker( TimeoutCheckerModel<T> timeoutCheckerModel )
        {
            if ( timeoutCheckerModel == null )
                throw new ArgumentNullException( nameof( timeoutCheckerModel ), "建構子參數不能為空值喔，請記得初始化。" );

            var target = timeoutCheckerModel.Target;
            _timeoutTime = timeoutCheckerModel.TimeoutTime;
            _onTimeoutAction = timeoutCheckerModel.OnTimeoutAction;
            _logger = timeoutCheckerModel.Logger;
            _timer = new Timer( OnTimeout, target, _timeoutTime, Timeout.Infinite );
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
            _logger?.Log( new LogModel
            {
                Time = DateTime.Now,
                Message = $"資料傳輸逾時，等待時間 : {_timeoutTime.ToString( )} 毫秒。"
            } );

            var target = obj as T;
            _onTimeoutAction?.Invoke( target );
        }
    }
}