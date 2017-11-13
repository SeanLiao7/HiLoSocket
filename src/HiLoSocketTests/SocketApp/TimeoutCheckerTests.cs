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
    public class FakeTarget
    {
        public string Result { get; set; } = "Operating";
    }

    [TestFixture]
    [Category( "TimeoutCheckerTests" )]
    public class TimeoutCheckerTests
    {
        public const int DelayTime = 50;
        public const int Timeout = 10;

        [Test]
        public void LoggerTest( )
        {
            var logger = Substitute.For<ILogger>( );
            var target = new FakeTarget( );
            CreateTimeoutChecker( target, logger );
            Thread.Sleep( DelayTime );
            logger.Received( ).Log( Arg.Any<LogModel>( ) );
        }

        [Test]
        public void NullInputTest( )
        {
            Should.Throw<ArgumentNullException>(
                ( ) => new TimeoutChecker<FakeTarget>( null ) );
        }

        [Test]
        public void OnTimeoutTest( )
        {
            var target = new FakeTarget( );
            var checker = CreateTimeoutChecker( target, null );
            Thread.Sleep( DelayTime );
            checker.StopChecking( );
            target.Result.ShouldBe( "Time out!" );
        }

        [Test]
        public void StopCheckingTest( )
        {
            var target = new FakeTarget( );
            var checker = CreateTimeoutChecker( target, null );
            checker.StopChecking( );
            Thread.Sleep( DelayTime );
            target.Result.ShouldBe( "Operating" );
        }

        private static TimeoutChecker<FakeTarget> CreateTimeoutChecker(
            FakeTarget fakeTarget,
            ILogger logger )
        {
            return new TimeoutChecker<FakeTarget>(
                new TimeoutCheckerModel<FakeTarget>
                {
                    Logger = logger,
                    OnTimeoutAction = x => x.Result = "Time out!",
                    Target = fakeTarget,
                    TimeoutTime = Timeout
                } );
        }
    }
}