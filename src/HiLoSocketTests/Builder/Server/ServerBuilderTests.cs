using System.ComponentModel.DataAnnotations;
using HiLoSocket.Builder.Server;
using NUnit.Framework;
using Shouldly;

namespace HiLoSocketTests.Builder.Server
{
    [TestFixture]
    public class ServerBuilderTests
    {
        [Test]
        public void BuildNullLoaclIpEndPointTest( )
        {
            Should.Throw<ValidationException>(
                ( ) => ServerBuilder<string>.CreateNew( )
                    .SetLocalIpEndPoint( null )
                    .SetFormatterType( null )
                    .SetCompressType( null )
                    .SetLogger( null )
                    .Build( ) );
        }
    }
}