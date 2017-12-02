using System;
using FluentAssertions;
using HiLoSocket.CommandFormatter;
using NUnit.Framework;
using Ploeh.AutoFixture;
using Shouldly;

namespace HiLoSocketTests.CommandFormatter.Implements
{
    [TestFixture]
    [Category( "JsonCommandFormatterTests" )]
    public class JsonCommandFormatterTests
    {
        [Test]
        public void Deserialize_NullInput_ThrowsArgumentNullException( )
        {
            var formatter = FormatterFactory<JsonDataObject>.CreateFormatter( FormatterType.JSonFormatter );
            var fixture = new Fixture( );
            fixture.Register<byte[ ]>( ( ) => null );
            var input = fixture.Create<byte[ ]>( );
            Should.Throw<ArgumentNullException>(
                ( ) => formatter.Deserialize( input ) );
        }

        [Test]
        public void Deserialize_ZeroLengthInput_ThrowsArgumentException( )
        {
            var formatter = FormatterFactory<JsonDataObject>.CreateFormatter( FormatterType.JSonFormatter );
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
            var formatter = FormatterFactory<JsonDataObject>.CreateFormatter( FormatterType.JSonFormatter );
            var fixture = new Fixture( );
            fixture.Register<JsonDataObject>( ( ) => null );
            var input = fixture.Create<JsonDataObject>( );
            Should.Throw<ArgumentNullException>(
                ( ) => formatter.Serialize( input ) );
        }

        [Test]
        public void SerializeAndDeSerialze_JsonDataObject_ShouldBeEqual( )
        {
            var formatter = FormatterFactory<JsonDataObject>.CreateFormatter( FormatterType.JSonFormatter );
            var fixture = new Fixture( );
            var expected = fixture.Create<JsonDataObject>( );
            var serializedResult = formatter.Serialize( expected );
            var actual = formatter.Deserialize( serializedResult );
            actual.ShouldBeEquivalentTo( expected );
        }

        [Test]
        public void SerializeAndDeSerialze_String_ShouldBeEqual( )
        {
            var formatter = FormatterFactory<string>.CreateFormatter( FormatterType.JSonFormatter );
            var fixture = new Fixture( );
            var expected = fixture.Create<string>( );
            var serializedResult = formatter.Serialize( expected );
            var actual = formatter.Deserialize( serializedResult );
            actual.ShouldBeEquivalentTo( expected );
        }

        public class JsonDataObject
        {
            public int Age { get; set; }
            public Guid Id { get; }
            public string Name { get; set; }

            public JsonDataObject( Guid id )
            {
                Id = id;
            }
        }
    }
}