using System;
using System.Runtime.Serialization;
using FluentAssertions;
using HiLoSocket.CommandFormatter;
using NUnit.Framework;
using Ploeh.AutoFixture;
using Shouldly;

namespace HiLoSocketTests.CommandFormatter.Implements
{
    [TestFixture]
    [Category( "BinaryCommandFormatterTests" )]
    public class BinaryCommandFormatterTests
    {
        [Test]
        public void Deserialize_NonSerializableDataObject_ThrowsSerializationException( )
        {
            var formatter = FormatterFactory<NonSerializableDataObject>.CreateFormatter( FormatterType.BinaryFormatter );
            var fixture = new Fixture
            {
                RepeatCount = 10
            };
            var input = fixture.Create<byte[ ]>( );
            Should.Throw<SerializationException>(
                ( ) => formatter.Deserialize( input ) );
        }

        [Test]
        public void Deserialize_NonSerializedSerializableDataObject_ThrowsSerializationException( )
        {
            var formatter = FormatterFactory<SerializableDataObject>.CreateFormatter( FormatterType.BinaryFormatter );
            var fixture = new Fixture
            {
                RepeatCount = 10
            };
            var input = fixture.Create<byte[ ]>( );
            Should.Throw<SerializationException>(
                ( ) => formatter.Deserialize( input ) );
        }

        [Test]
        public void Deserialize_NullInput_ThrowsArgumentNullException( )
        {
            var formatter = FormatterFactory<SerializableDataObject>.CreateFormatter( FormatterType.BinaryFormatter );
            var fixture = new Fixture( );
            fixture.Register<byte[ ]>( ( ) => null );
            var input = fixture.Create<byte[ ]>( );
            Should.Throw<ArgumentNullException>(
                ( ) => formatter.Deserialize( input ) );
        }

        [Test]
        public void Deserialize_ZeroLengthInput_ThrowsArgumentException( )
        {
            var formatter = FormatterFactory<SerializableDataObject>.CreateFormatter( FormatterType.BinaryFormatter );
            var fixture = new Fixture
            {
                RepeatCount = 0
            };
            var input = fixture.Create<byte[ ]>( );
            Should.Throw<ArgumentException>(
                ( ) => formatter.Deserialize( input ) );
        }

        [Test]
        public void Serialize_NonSerializableDataObject_ThrowsSerializationException( )
        {
            var formatter = FormatterFactory<NonSerializableDataObject>.CreateFormatter( FormatterType.BinaryFormatter );
            var fixture = new Fixture( );
            var input = fixture.Create<NonSerializableDataObject>( );
            Should.Throw<SerializationException>(
                ( ) => formatter.Serialize( input ) );
        }

        [Test]
        public void Serialize_NullInput_ThrowsArgumentNullException( )
        {
            var formatter = FormatterFactory<SerializableDataObject>.CreateFormatter( FormatterType.BinaryFormatter );
            var fixture = new Fixture( );
            fixture.Register<SerializableDataObject>( ( ) => null );
            var input = fixture.Create<SerializableDataObject>( );
            Should.Throw<ArgumentNullException>(
                ( ) => formatter.Serialize( input ) );
        }

        [Test]
        public void SerializeAndDeserialize_SerializableDataObject_ShouldBeEqual( )
        {
            var formatter = FormatterFactory<SerializableDataObject>.CreateFormatter( FormatterType.BinaryFormatter );
            var fixture = new Fixture( );
            var expected = fixture.Create<SerializableDataObject>( );
            var serializedResult = formatter.Serialize( expected );
            var actual = formatter.Deserialize( serializedResult );
            actual.ShouldBeEquivalentTo( expected );
        }

        [Test]
        public void SerializeAndDeserialize_String_ShouldBeEqual( )
        {
            var formatter = FormatterFactory<string>.CreateFormatter( FormatterType.BinaryFormatter );
            var fixture = new Fixture( );
            var expected = fixture.Create<string>( );
            var serializedResult = formatter.Serialize( expected );
            var actual = formatter.Deserialize( serializedResult );
            actual.ShouldBeEquivalentTo( expected );
        }

        public class NonSerializableDataObject
        {
            public string Name { get; set; }
        }

        [Serializable]
        public class SerializableDataObject
        {
            public int Age { get; set; }
            public Guid Id { get; }
            public string Name { get; set; }

            public SerializableDataObject( Guid id )
            {
                Id = id;
            }
        }
    }
}