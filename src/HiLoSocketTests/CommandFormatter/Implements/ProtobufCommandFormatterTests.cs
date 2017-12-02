using System;
using FluentAssertions;
using HiLoSocket.CommandFormatter;
using NUnit.Framework;
using Ploeh.AutoFixture;
using ProtoBuf;
using Shouldly;

namespace HiLoSocketTests.CommandFormatter.Implements
{
    [TestFixture]
    [Category( "ProtobufCommandFormatterTests" )]
    public class ProtobufCommandFormatterTests
    {
        [Test]
        public void Deserialize_NullInput_ThrowsArgumentNullException( )
        {
            var formatter = FormatterFactory<ProtobufDataObject>.CreateFormatter( FormatterType.ProtobufFormatter );
            var fixture = new Fixture( );
            fixture.Register<byte[ ]>( ( ) => null );
            var input = fixture.Create<byte[ ]>( );
            Should.Throw<ArgumentNullException>(
                ( ) => formatter.Deserialize( input ) );
        }

        [Test]
        public void Deserialize_ZeroLengthInput_ThrowsArgumentException( )
        {
            var formatter = FormatterFactory<ProtobufDataObject>.CreateFormatter( FormatterType.ProtobufFormatter );
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
            var formatter = FormatterFactory<ProtobufDataObject>.CreateFormatter( FormatterType.ProtobufFormatter );
            var fixture = new Fixture( );
            fixture.Register<ProtobufDataObject>( ( ) => null );
            var input = fixture.Create<ProtobufDataObject>( );
            Should.Throw<ArgumentNullException>(
                ( ) => formatter.Serialize( input ) );
        }

        [Test]
        public void SerializeAndDeserialize_ProtobufDataObject_ShouldBeEqual( )
        {
            var formatter = FormatterFactory<ProtobufDataObject>.CreateFormatter( FormatterType.ProtobufFormatter );
            var fixture = new Fixture( );
            var expected = fixture.Create<ProtobufDataObject>( );
            var serializedResult = formatter.Serialize( expected );
            var actual = formatter.Deserialize( serializedResult );
            actual.ShouldBeEquivalentTo( expected );
        }

        public void SerializeAndDeserialize_String_ShouldBeEqual( )
        {
            var formatter = FormatterFactory<string>.CreateFormatter( FormatterType.ProtobufFormatter );
            var fixture = new Fixture( );
            var expected = fixture.Create<string>( );
            var serializedResult = formatter.Serialize( expected );
            var actual = formatter.Deserialize( serializedResult );
            actual.ShouldBeEquivalentTo( expected );
        }

        [ProtoContract]
        public class ProtobufDataObject
        {
            [ProtoMember( 1 )]
            public int Age { get; set; }

            [ProtoMember( 2 )]
            public Guid Id { get; }

            [ProtoMember( 3 )]
            public string Name { get; set; }

            public ProtobufDataObject( Guid id )
            {
                Id = id;
            }

            private ProtobufDataObject( )
            {
            }
        }
    }
}