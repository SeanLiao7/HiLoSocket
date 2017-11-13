using System;
using System.Threading;
using HiLoSocket.Logger;
using HiLoSocket.Model;
using HiLoSocket.Model.InternalOnly;
using HiLoSocket.SocketApp;
using NSubstitute;
using NUnit.Framework;
using Shouldly;

namespace HiLoSocketTests.SocketApp
{
    [TestFixture]
    [Category( "TimeoutCheckerTests" )]
    public class TimeoutCheckerTests
    {
        public const int DelayTime = 50;
        public const int Timeout = 10;

        [Test]
        public void InstantiateTimeoutChecker_NullInput_ThrowsArgumentNullException( )
        {
            Should.Throw<ArgumentNullException>(
                ( ) => new TimeoutChecker<MockObject>( null ) );
        }

        [Test]
        public void OnTimeout_Logger_LogShouldBeCalledOnce( )
        {
            var logger = Substitute.For<ILogger>( );
            var mockObj = new MockObject( );
            CreateTimeoutChecker( mockObj, logger );
            Thread.Sleep( DelayTime );
            logger.Received( ).Log( Arg.Any<LogModel>( ) );
        }

        [Test]
        public void StopCheckingCalledAfterTimeout_MockObject_ShoulBeModified( )
        {
            var mockObj = new MockObject( );
            var checker = CreateTimeoutChecker( mockObj, null );
            Thread.Sleep( DelayTime );
            checker.StopChecking( );
            mockObj.Result.ShouldBe( "Time out!" );
        }

        [Test]
        public void StopCheckingCalledBeforeTimeout_MockObject_ShouldNotBeModified( )
        {
            var mockObj = new MockObject( );
            var checker = CreateTimeoutChecker( mockObj, null );
            checker.StopChecking( );
            Thread.Sleep( DelayTime );
            mockObj.Result.ShouldBe( "Operating" );
        }

        private static TimeoutChecker<MockObject> CreateTimeoutChecker(
            MockObject mockObject,
            ILogger logger )
        {
            return new TimeoutChecker<MockObject>(
                new TimeoutCheckerModel<MockObject>
                {
                    Logger = logger,
                    OnTimeoutAction = x => x.Result = "Time out!",
                    Target = mockObject,
                    TimeoutTime = Timeout
                } );
        }

        private class MockObject
        {
            public string Result { get; set; } = "Operating";
        }
    }
}