using System;
using System.Linq;
using System.Runtime.Serialization;
using HiLoSocket.CommandFormatter;
using NUnit.Framework;

namespace HiLoSocketTests.CommandFormatter.Implements
{
    [TestFixture]
    public class BinaryCommandFormatterTests
    {
        [Test]
        public void DeserializeNonSerializedTest( )
        {
            var formatter = FormatterFactory<NonSerializedData>.CreateFormatter( FormatterType.BinaryFormatter );
            var input = new byte[ 1 ];
            Assert.Throws(
                typeof( SerializationException ),
                ( ) => formatter.Deserialize( input ) );
        }

        [Test]
        public void DeserializeNullInputTest( )
        {
            var formatter = FormatterFactory<NonSerializedData>.CreateFormatter( FormatterType.BinaryFormatter );
            Assert.Throws(
                typeof( ArgumentNullException ),
                ( ) => formatter.Deserialize( null ) );
        }

        [Test]
        public void DeserializeZeroLengthTest( )
        {
            var formatter = FormatterFactory<NonSerializedData>.CreateFormatter( FormatterType.BinaryFormatter );
            var input = new byte[ 0 ];
            Assert.Throws(
                typeof( ArgumentException ),
                ( ) => formatter.Deserialize( input ) );
        }

        [Test]
        public void SerializebyteArrayTest( )
        {
            var formatter = FormatterFactory<byte[ ]>.CreateFormatter( FormatterType.BinaryFormatter );
            var expected = new byte[ ] { 65, 44, 251, 0, 1, 196, 111, 39, 222, 88 };
            var serializedResult = formatter.Serialize( expected );
            var actual = formatter.Deserialize( serializedResult );
            var areEqual = expected.SequenceEqual( actual );
            Assert.IsTrue( areEqual );
        }

        [Test]
        public void SerializeNonSerializedTest( )
        {
            var formatter = FormatterFactory<NonSerializedData>.CreateFormatter( FormatterType.BinaryFormatter );
            var input = new NonSerializedData
            {
                Name = "Tom"
            };
            Assert.Throws(
                typeof( SerializationException ),
                ( ) => formatter.Serialize( input ) );
        }

        [Test]
        public void SerializeNullInputTest( )
        {
            var formatter = FormatterFactory<NonSerializedData>.CreateFormatter( FormatterType.BinaryFormatter );
            Assert.Throws(
                typeof( ArgumentNullException ),
                ( ) => formatter.Serialize( null ) );
        }

        [Test]
        public void SerializeObjectTest( )
        {
            var formatter = FormatterFactory<SerializedData>.CreateFormatter( FormatterType.BinaryFormatter );
            var expected = new SerializedData
            {
                Name = "Penny",
                Age = 20
            };
            var serializedResult = formatter.Serialize( expected );
            var actual = formatter.Deserialize( serializedResult );
            Assert.AreEqual( expected, actual );
        }

        [Test]
        public void SerializeStringTest( )
        {
            var formatter = FormatterFactory<string>.CreateFormatter( FormatterType.BinaryFormatter );
            const string expected = "*測試 Test#_$% ?";
            var serializedResult = formatter.Serialize( expected );
            var actual = formatter.Deserialize( serializedResult );
            Assert.AreEqual( expected, actual );
        }

        public class NonSerializedData
        {
            public string Name { get; set; }
        }

        [Serializable]
        public class SerializedData : IEquatable<SerializedData>
        {
            public int Age { get; set; }
            public Guid Id { get; } = Guid.NewGuid( );
            public string Name { get; set; }

            public static bool operator !=( SerializedData a, SerializedData b )
            {
                return !( a == b );
            }

            public static bool operator ==( SerializedData a, SerializedData b )
            {
                return Equals( a, b );
            }

            public bool Equals( SerializedData other )
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
                    && Equals( ( SerializedData ) obj );
            }

            public override int GetHashCode( )
            {
                return Id.GetHashCode( );
            }
        }
    }
}