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
    internal class TimeoutChecker<T>
        where T : class
    {
        public ILogger Logger { get; }
        public Action<T> OnTimeoutAction { get; }
        public int TimeoutTime { get; }
        public Timer Timer { get; }

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
            TimeoutTime = timeoutCheckerModel.TimeoutTime;
            OnTimeoutAction = timeoutCheckerModel.OnTimeoutAction;
            Logger = timeoutCheckerModel.Logger;
            Timer = new Timer( OnTimeout, target, TimeoutTime, Timeout.Infinite );
        }

        /// <summary>
        /// Stops checking.
        /// </summary>
        public void StopChecking( )
        {
            Timer.Dispose( );
        }

        private void OnTimeout( object obj )
        {
            Logger?.Log( new LogModel
            {
                Time = DateTime.Now,
                Message = $"資料傳輸逾時，等待時間 : {TimeoutTime.ToString( )} 毫秒。"
            } );

            var target = obj as T;
            OnTimeoutAction?.Invoke( target );
        }
    }
}