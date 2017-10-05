using System;
using HiLoSocket.CommandFormatter;
using NUnit.Framework;
using ProtoBuf;
using Shouldly;

namespace HiLoSocketTests.CommandFormatter.Implements
{
    [TestFixture]
    public class ProtobufCommandFormatterTests
    {
        [Test]
        public void DeserializeNullInputTest( )
        {
            var formatter = FormatterFactory<ProtobufData>.CreateFormatter( FormatterType.ProtobufFormatter );
            Should.Throw<ArgumentNullException>(
                ( ) => formatter.Deserialize( null ) );
        }

        [Test]
        public void DeserializeZeroLengthTest( )
        {
            var formatter = FormatterFactory<ProtobufData>.CreateFormatter( FormatterType.ProtobufFormatter );
            var input = new byte[ 0 ];
            Should.Throw<ArgumentException>(
                ( ) => formatter.Deserialize( input ) );
        }

        [Test]
        public void SerializeNullInputTest( )
        {
            var formatter = FormatterFactory<ProtobufData>.CreateFormatter( FormatterType.ProtobufFormatter );
            Should.Throw<ArgumentNullException>(
                ( ) => formatter.Serialize( null ) );
        }

        [Test]
        public void SerializeObjectTest( )
        {
            var formatter = FormatterFactory<ProtobufData>.CreateFormatter( FormatterType.ProtobufFormatter );
            var expected = new ProtobufData( Guid.NewGuid( ) )
            {
                Name = "Penny",
                Age = 20
            };
            var serializedResult = formatter.Serialize( expected );
            var actual = formatter.Deserialize( serializedResult );
            actual.ShouldBe( expected );
        }

        [Test]
        [TestCase( "*測試 Test#_$% ?" )]
        [TestCase( "*測試 0    ZZp $% ? ●" )]
        [TestCase( "* 測 → 】試 0 『 Y Z　＠ ●" )]
        public void SerializeStringTest( string expected )
        {
            var formatter = FormatterFactory<string>.CreateFormatter( FormatterType.ProtobufFormatter );
            var serializedResult = formatter.Serialize( expected );
            var actual = formatter.Deserialize( serializedResult );
            actual.ShouldBe( expected );
        }
    }

    [ProtoContract]
    public class ProtobufData : IEquatable<ProtobufData>
    {
        [ProtoMember( 1 )]
        public int Age { get; set; }

        [ProtoMember( 2 )]
        public Guid Id { get; }

        [ProtoMember( 3 )]
        public string Name { get; set; }

        public ProtobufData( Guid id )
        {
            Id = id;
        }

        private ProtobufData( )
        {
        }

        public static bool operator !=( ProtobufData a, ProtobufData b )
        {
            return !( a == b );
        }

        public static bool operator ==( ProtobufData a, ProtobufData b )
        {
            return Equals( a, b );
        }

        public bool Equals( ProtobufData other )
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
                   && Equals( ( ProtobufData ) obj );
        }

        public override int GetHashCode( )
        {
            return Id.GetHashCode( );
        }
    }
}