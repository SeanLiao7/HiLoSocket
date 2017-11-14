using System.ComponentModel.DataAnnotations;
using HiLoSocket.Builder.Server;
using NUnit.Framework;
using Shouldly;

namespace HiLoSocketTests.Builder.Server
{
    [TestFixture]
    [Category( "ServerBuilderTests" )]
    public class ServerBuilderTests
    {
        [Test]
        public void Build_NullLocalIpEndPoint_ThrowsValidationException( )
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