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
    public class TimeoutCheckerTests
    {
        [Test]
        public void TimeoutCheckerLoggerTest( )
        {
            const string expected = "Message logged";
            var actual = "";
            var logger = Substitute.For<ILogger>( );
            logger.When(
                x => x.Log( Arg.Any<LogModel>( ) ) )
                .Do( info =>
                {
                    actual = "Message logged";
                } );

            var target = new FakeTarget( );
            var checker = new TimeoutChecker<FakeTarget>(
                new TimeoutCheckerModel<FakeTarget>
                {
                    Logger = logger,
                    OnTimeoutAction = null,
                    Target = target,
                    TimeoutTime = 10
                } );
            Thread.Sleep( 50 );
            actual.ShouldBe( expected );
        }

        [Test]
        public void TimeoutCheckerNullInputTest( )
        {
            Should.Throw<ArgumentNullException>(
                ( ) => new TimeoutChecker<FakeTarget>( null ) );
        }

        [Test]
        public void TimeoutCheckerStopCheckingTest( )
        {
            var target = new FakeTarget( );
            var checker = CreateTimeroutChecker( target );
            checker.StopChecking( );
            Thread.Sleep( 50 );
            target.Result.ShouldBe( "Operating" );
        }

        [Test]
        public void TimeoutCheckerTimeoutTest( )
        {
            var target = new FakeTarget( );
            var checker = CreateTimeroutChecker( target );
            Thread.Sleep( 50 );
            checker.StopChecking( );
            target.Result.ShouldBe( "Time out!" );
        }

        private static TimeoutChecker<FakeTarget> CreateTimeroutChecker( FakeTarget fakeTarget )
        {
            return new TimeoutChecker<FakeTarget>(
                new TimeoutCheckerModel<FakeTarget>
                {
                    Logger = null,
                    OnTimeoutAction = x => x.Result = "Time out!",
                    Target = fakeTarget,
                    TimeoutTime = 10
                } );
        }
    }
}