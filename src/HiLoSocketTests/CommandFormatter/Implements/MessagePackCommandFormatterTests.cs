using System;
using FluentAssertions;
using HiLoSocket.CommandFormatter;
using NUnit.Framework;
using Ploeh.AutoFixture;
using Shouldly;

namespace HiLoSocketTests.CommandFormatter.Implements
{
    [TestFixture]
    [Category( "MessagePackCommandFormatterTests" )]
    public class MessagePackCommandFormatterTests
    {
        [Test]
        public void Deserialize_NullInput_ThrowsArgumentNullException( )
        {
            var formatter = FormatterFactory<MessagePackDataObject>.CreateFormatter( FormatterType.MessagePackFormatter );
            var fixture = new Fixture( );
            fixture.Register<byte[ ]>( ( ) => null );
            var input = fixture.Create<byte[ ]>( );
            Should.Throw<ArgumentNullException>(
                ( ) => formatter.Deserialize( input ) );
        }

        [Test]
        public void Deserialize_ZeroLengthInput_ThrowsArgumentException( )
        {
            var formatter = FormatterFactory<MessagePackDataObject>.CreateFormatter( FormatterType.MessagePackFormatter );
            var fixture = new Fixture
            {
                RepeatCount = 0
            };
            var input = fixture.Create<byte[ ]>( );
            Should.Throw<ArgumentException>(
                ( ) => formatter.Deserialize( input ) );
        }

        [Test]
        public void Serialize_NullInput_ThrowsArgumentNullException( )
        {
            var formatter = FormatterFactory<MessagePackDataObject>.CreateFormatter( FormatterType.MessagePackFormatter );
            var fixture = new Fixture( );
            fixture.Register<MessagePackDataObject>( ( ) => null );
            var input = fixture.Create<MessagePackDataObject>( );
            Should.Throw<ArgumentNullException>(
                ( ) => formatter.Serialize( input ) );
        }

        [Test]
        public void SerializeAndDeserialize_MessagePackDataObject_ShouldBeEqual( )
        {
            var formatter = FormatterFactory<MessagePackDataObject>.CreateFormatter( FormatterType.MessagePackFormatter );
            var fixture = new Fixture( );
            var expected = fixture.Create<MessagePackDataObject>( );
            var serializedResult = formatter.Serialize( expected );
            var actual = formatter.Deserialize( serializedResult );
            actual.ShouldBeEquivalentTo( expected );
        }

        [Test]
        public void SerializeAndDeserialize_String_ShouldBeEqual( )
        {
            var formatter = FormatterFactory<string>.CreateFormatter( FormatterType.MessagePackFormatter );
            var fixture = new Fixture( );
            var expected = fixture.Create<string>( );
            var serializedResult = formatter.Serialize( expected );
            var actual = formatter.Deserialize( serializedResult );
            actual.ShouldBeEquivalentTo( expected );
        }

        public class MessagePackDataObject
        {
            public int Age { get; set; }
            public Guid Id { get; }
            public string Name { get; set; }

            public MessagePackDataObject( Guid id )
            {
                Id = id;
            }
        }
    }
}