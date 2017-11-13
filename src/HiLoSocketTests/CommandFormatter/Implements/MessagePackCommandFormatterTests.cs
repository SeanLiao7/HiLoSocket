using System;
using HiLoSocket.CommandFormatter;
using NUnit.Framework;
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
            Should.Throw<ArgumentNullException>(
                ( ) => formatter.Deserialize( null ) );
        }

        [Test]
        public void Deserialize_ZeroLengthInput_ThrowsArgumentException( )
        {
            var formatter = FormatterFactory<MessagePackDataObject>.CreateFormatter( FormatterType.MessagePackFormatter );
            var input = new byte[ 0 ];
            Should.Throw<ArgumentException>(
                ( ) => formatter.Deserialize( input ) );
        }

        [Test]
        public void Serialize_NullInput_ThrowsArgumentNullException( )
        {
            var formatter = FormatterFactory<MessagePackDataObject>.CreateFormatter( FormatterType.MessagePackFormatter );
            Should.Throw<ArgumentNullException>(
                ( ) => formatter.Serialize( null ) );
        }

        [Test]
        public void SerializeAndDeserialize_MessagePackDataObject_ShouldBeEqual( )
        {
            var formatter = FormatterFactory<MessagePackDataObject>.CreateFormatter( FormatterType.MessagePackFormatter );
            var expected = new MessagePackDataObject( Guid.NewGuid( ) )
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
            var formatter = FormatterFactory<string>.CreateFormatter( FormatterType.MessagePackFormatter );
            var serializedResult = formatter.Serialize( expected );
            var actual = formatter.Deserialize( serializedResult );
            actual.ShouldBe( expected );
        }

        public class MessagePackDataObject : IEquatable<MessagePackDataObject>
        {
            public int Age { get; set; }
            public Guid Id { get; }
            public string Name { get; set; }

            public MessagePackDataObject( Guid id )
            {
                Id = id;
            }

            public static bool operator !=( MessagePackDataObject a, MessagePackDataObject b )
            {
                return !( a == b );
            }

            public static bool operator ==( MessagePackDataObject a, MessagePackDataObject b )
            {
                return Equals( a, b );
            }

            public bool Equals( MessagePackDataObject other )
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
                       && Equals( ( MessagePackDataObject ) obj );
            }

            public override int GetHashCode( )
            {
                return Id.GetHashCode( );
            }
        }
    }
}