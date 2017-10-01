using System;
using HiLoSocket.CommandFormatter;
using NUnit.Framework;

namespace HiLoSocketTests.CommandFormatter.Implements
{
    [TestFixture]
    public class MessagePackCommandFormatterTests
    {
        [Test]
        public void DeserializeNullInputTest( )
        {
            var formatter = FormatterFactory<MessagePackData>.CreateFormatter( FormatterType.MessagePackFormatter );
            Assert.Throws(
                typeof( ArgumentNullException ),
                ( ) => formatter.Deserialize( null ) );
        }

        [Test]
        public void DeserializeZeroLengthTest( )
        {
            var formatter = FormatterFactory<MessagePackData>.CreateFormatter( FormatterType.MessagePackFormatter );
            var input = new byte[ 0 ];
            Assert.Throws(
                typeof( ArgumentException ),
                ( ) => formatter.Deserialize( input ) );
        }

        [Test]
        public void SerializeNullInputTest( )
        {
            var formatter = FormatterFactory<MessagePackData>.CreateFormatter( FormatterType.MessagePackFormatter );
            Assert.Throws(
                typeof( ArgumentNullException ),
                ( ) => formatter.Serialize( null ) );
        }

        [Test]
        public void SerializeObjectTest( )
        {
            var formatter = FormatterFactory<MessagePackData>.CreateFormatter( FormatterType.MessagePackFormatter );
            var expected = new MessagePackData( Guid.NewGuid( ) )
            {
                Name = "Penny",
                Age = 20
            };
            var serializedResult = formatter.Serialize( expected );
            var actual = formatter.Deserialize( serializedResult );
            Assert.AreEqual( expected, actual );
        }

        [Test]
        [TestCase( "*測試 Test#_$% ?" )]
        [TestCase( "*測試 0    ZZp $% ? ●" )]
        [TestCase( "* 測 → 】試 0 『 Y Z　＠ ●" )]
        public void SerializeStringTest( string expected )
        {
            var formatter = FormatterFactory<string>.CreateFormatter( FormatterType.MessagePackFormatter );
            var serializedResult = formatter.Serialize( expected );
            var actual = formatter.Deserialize( serializedResult );
            Assert.AreEqual( expected, actual );
        }

        public class MessagePackData : IEquatable<MessagePackData>
        {
            public int Age { get; set; }
            public Guid Id { get; }
            public string Name { get; set; }

            public MessagePackData( Guid id )
            {
                Id = id;
            }

            public static bool operator !=( MessagePackData a, MessagePackData b )
            {
                return !( a == b );
            }

            public static bool operator ==( MessagePackData a, MessagePackData b )
            {
                return Equals( a, b );
            }

            public bool Equals( MessagePackData other )
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
                    && Equals( ( MessagePackData ) obj );
            }

            public override int GetHashCode( )
            {
                return Id.GetHashCode( );
            }
        }
    }
}