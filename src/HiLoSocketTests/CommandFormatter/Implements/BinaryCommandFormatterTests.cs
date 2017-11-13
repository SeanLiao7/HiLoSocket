using System;
using System.Runtime.Serialization;
using HiLoSocket.CommandFormatter;
using NUnit.Framework;
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
            var input = new byte[ 1 ];
            Should.Throw<SerializationException>(
                ( ) => formatter.Deserialize( input ) );
        }

        [Test]
        public void Deserialize_NonSerializedSerializableDataObject_ThrowsSerializationException( )
        {
            var formatter = FormatterFactory<SerializableDataObject>.CreateFormatter( FormatterType.BinaryFormatter );
            var input = new byte[ 1 ];
            Should.Throw<SerializationException>(
                ( ) => formatter.Deserialize( input ) );
        }

        [Test]
        public void Deserialize_NullInput_ThrowsArgumentNullException( )
        {
            var formatter = FormatterFactory<SerializableDataObject>.CreateFormatter( FormatterType.BinaryFormatter );
            Should.Throw<ArgumentNullException>(
                ( ) => formatter.Deserialize( null ) );
        }

        [Test]
        public void Deserialize_ZeroLengthInput_ThrowsArgumentException( )
        {
            var formatter = FormatterFactory<SerializableDataObject>.CreateFormatter( FormatterType.BinaryFormatter );
            var input = new byte[ 0 ];
            Should.Throw<ArgumentException>(
                ( ) => formatter.Deserialize( input ) );
        }

        [Test]
        public void Serialize_NonSerializableDataObject_ThrowsSerializationException( )
        {
            var formatter = FormatterFactory<NonSerializableDataObject>.CreateFormatter( FormatterType.BinaryFormatter );
            var input = new NonSerializableDataObject
            {
                Name = "Tom"
            };
            Should.Throw<SerializationException>(
                ( ) => formatter.Serialize( input ) );
        }

        [Test]
        public void Serialize_NullInput_ThrowsArgumentNullException( )
        {
            var formatter = FormatterFactory<SerializableDataObject>.CreateFormatter( FormatterType.BinaryFormatter );
            Should.Throw<ArgumentNullException>(
                ( ) => formatter.Serialize( null ) );
        }

        [Test]
        public void SerializeAndDeserialize_SerializableDataObject_ShouldBeEqual( )
        {
            var formatter = FormatterFactory<SerializableDataObject>.CreateFormatter( FormatterType.BinaryFormatter );
            var expected = new SerializableDataObject
            {
                Name = "Penny",
                Age = 20
            };
            var serializedResult = formatter.Serialize( expected );
            var actual = formatter.Deserialize( serializedResult );
            actual.ShouldBe( expected );
        }

        [TestCase( "*測試 Test#_$% ?" )]
        [TestCase( "*測試 0    ZZp $% ? ●" )]
        [TestCase( "* 測 → 】試 0 『 Y Z　＠ ●" )]
        public void SerializeAndDeserialize_String_ShouldBeEqual( string expected )
        {
            var formatter = FormatterFactory<string>.CreateFormatter( FormatterType.BinaryFormatter );
            var serializedResult = formatter.Serialize( expected );
            var actual = formatter.Deserialize( serializedResult );
            actual.ShouldBe( expected );
        }

        private class NonSerializableDataObject
        {
            public string Name { get; set; }
        }

        [Serializable]
        private class SerializableDataObject : IEquatable<SerializableDataObject>
        {
            public int Age { get; set; }
            public Guid Id { get; } = Guid.NewGuid( );
            public string Name { get; set; }

            public static bool operator !=( SerializableDataObject a, SerializableDataObject b )
            {
                return !( a == b );
            }

            public static bool operator ==( SerializableDataObject a, SerializableDataObject b )
            {
                return Equals( a, b );
            }

            public bool Equals( SerializableDataObject other )
            {
                if ( ReferenceEquals( null, other ) ) return false;
                if ( ReferenceEquals( this, other ) ) return true;
                return Id.Equals( other.Id )
                       && Name.Equals( other.Name )
                       && Age.Equals( other.Age );
            }

            public override bool Equals( object obj )
            {
                if ( ReferenceEquals( null, obj ) ) return false;
                if ( ReferenceEquals( this, obj ) ) return true;
                return obj.GetType( ) == GetType( )
                       && Equals( ( SerializableDataObject ) obj );
            }

            public override int GetHashCode( )
            {
                return Id.GetHashCode( );
            }
        }
    }
}