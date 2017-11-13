using System;
using HiLoSocket.CommandFormatter;
using NUnit.Framework;
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
            Should.Throw<ArgumentNullException>(
                ( ) => formatter.Deserialize( null ) );
        }

        [Test]
        public void Deserialize_ZeroLengthInput_ThrowsArgumentException( )
        {
            var formatter = FormatterFactory<JsonDataObject>.CreateFormatter( FormatterType.JSonFormatter );
            var input = new byte[ 0 ];
            Should.Throw<ArgumentException>(
                ( ) => formatter.Deserialize( input ) );
        }

        [Test]
        public void Serialize_NullInput_ThrowsArgumentNullException( )
        {
            var formatter = FormatterFactory<JsonDataObject>.CreateFormatter( FormatterType.JSonFormatter );
            Should.Throw<ArgumentNullException>(
                ( ) => formatter.Serialize( null ) );
        }

        [Test]
        public void SerializeAndDeSerialze_JsonDataObject_ShouldBeEqual( )
        {
            var formatter = FormatterFactory<JsonDataObject>.CreateFormatter( FormatterType.JSonFormatter );
            var expected = new JsonDataObject( Guid.NewGuid( ) )
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
        public void SerializeAndDeSerialze_String_ShouldBeEqual( string expected )
        {
            var formatter = FormatterFactory<string>.CreateFormatter( FormatterType.JSonFormatter );
            var serializedResult = formatter.Serialize( expected );
            var actual = formatter.Deserialize( serializedResult );
            actual.ShouldBe( expected );
        }

        private class JsonDataObject : IEquatable<JsonDataObject>
        {
            public int Age { get; set; }
            public Guid Id { get; }
            public string Name { get; set; }

            public JsonDataObject( Guid id )
            {
                Id = id;
            }

            public static bool operator !=( JsonDataObject a, JsonDataObject b )
            {
                return !( a == b );
            }

            public static bool operator ==( JsonDataObject a, JsonDataObject b )
            {
                return Equals( a, b );
            }

            public bool Equals( JsonDataObject other )
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
                       && Equals( ( JsonDataObject ) obj );
            }

            public override int GetHashCode( )
            {
                return Id.GetHashCode( );
            }
        }
    }
}