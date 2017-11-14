using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Net;
using HiLoSocket.Builder.Client;
using HiLoSocket.Compressor;
using NUnit.Framework;
using Shouldly;

namespace HiLoSocketTests.Builder.Client
{
    [TestFixture]
    [Category( "ClientBuilderTests" )]
    public class ClientBuilderTests
    {
        [TestCaseSource( typeof( TimeoutTimeSource ) )]
        public void Build_InvalidTimeoutTime_ThrowsValidationException( int timeoutTime )
        {
            Should.Throw<ValidationException>(
                ( ) => ClientBuilder<string>.CreateNew( )
                    .SetLocalIpEndPoint( new IPEndPoint( IPAddress.Parse( "127.0.0.1" ), 8000 ) )
                    .SetRemoteIpEndPoint( new IPEndPoint( IPAddress.Parse( "127.0.0.1" ), 8080 ) )
                    .SetFormatterType( null )
                    .SetCompressType( null )
                    .SetTimeoutTime( timeoutTime )
                    .SetLogger( null )
                    .Build( ) );
        }

        [Test]
        public void Build_NullLocalIpEndPoint_ThrowsValidationException( )
        {
            Should.Throw<ValidationException>(
                ( ) => ClientBuilder<string>.CreateNew( )
                    .SetLocalIpEndPoint( null )
                    .SetRemoteIpEndPoint( new IPEndPoint( IPAddress.Parse( "127.0.0.1" ), 8000 ) )
                    .SetFormatterType( null )
                    .SetCompressType( null )
                    .SetTimeoutTime( 5000 )
                    .SetLogger( null )
                    .Build( ) );
        }

        [Test]
        public void Build_NullRemoteIpEndPoint_ThrowsValidationException( )
        {
            Should.Throw<ValidationException>(
                ( ) => ClientBuilder<string>.CreateNew( )
                    .SetLocalIpEndPoint( new IPEndPoint( IPAddress.Parse( "127.0.0.1" ), 8000 ) )
                    .SetRemoteIpEndPoint( null )
                    .SetFormatterType( null )
                    .SetCompressType( null )
                    .SetTimeoutTime( 5000 )
                    .SetLogger( null )
                    .Build( ) );
        }

        private class TimeoutTimeSource : IEnumerable<int>
        {
            public IEnumerator<int> GetEnumerator( )
            {
                yield return -2000;
                yield return 0;
                yield return 300000;
            }

            IEnumerator IEnumerable.GetEnumerator( )
            {
                return GetEnumerator( );
            }
        }
    }
}