using System;
using System.Collections;
using System.Collections.Generic;
using HiLoSocket.CommandFormatter;
using NUnit.Framework;
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
            Should.Throw<ArgumentNullException>(
                ( ) => formatter.Deserialize( null ) );
        }

        [Test]
        public void Deserialize_ZeroLengthInput_ThrowsArgumentException( )
        {
            var formatter = FormatterFactory<ProtobufDataObject>.CreateFormatter( FormatterType.ProtobufFormatter );
            var input = new byte[ 0 ];
            Should.Throw<ArgumentException>(
                ( ) => formatter.Deserialize( input ) );
        }

        [Test]
        public void Serialize_NullInput_ThrowsArgumentNullException( )
        {
            var formatter = FormatterFactory<ProtobufDataObject>.CreateFormatter( FormatterType.ProtobufFormatter );
            Should.Throw<ArgumentNullException>(
                ( ) => formatter.Serialize( null ) );
        }

        [Test]
        public void SerializeAndDeserialize_ProtobufDataObject_ShouldBeEqual( )
        {
            var formatter = FormatterFactory<ProtobufDataObject>.CreateFormatter( FormatterType.ProtobufFormatter );
            var expected = new ProtobufDataObject( Guid.NewGuid( ) )
            {
                Name = "Penny",
                Age = 20
            };
            var serializedResult = formatter.Serialize( expected );
            var actual = formatter.Deserialize( serializedResult );
            actual.ShouldBe( expected );
        }

        [TestCaseSource( typeof( StringSource ) )]
        public void SerializeAndDeserialize_String_ShouldBeEqual( string expected )
        {
            var formatter = FormatterFactory<string>.CreateFormatter( FormatterType.ProtobufFormatter );
            var serializedResult = formatter.Serialize( expected );
            var actual = formatter.Deserialize( serializedResult );
            actual.ShouldBe( expected );
        }

        [ProtoContract]
        private class ProtobufDataObject : IEquatable<ProtobufDataObject>
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

            public static bool operator !=( ProtobufDataObject a, ProtobufDataObject b )
            {
                return !( a == b );
            }

            public static bool operator ==( ProtobufDataObject a, ProtobufDataObject b )
            {
                return Equals( a, b );
            }

            public bool Equals( ProtobufDataObject other )
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
                       && Equals( ( ProtobufDataObject ) obj );
            }

            public override int GetHashCode( )
            {
                return Id.GetHashCode( );
            }
        }

        private class StringSource : IEnumerable<string>
        {
            public IEnumerator<string> GetEnumerator( )
            {
                yield return "*測試 Test#_$% ?";
                yield return "*測試 0    ZZp $% ? ●";
                yield return "* 測 → 】試 0 『 Y Z　＠ ●";
            }

            IEnumerator IEnumerable.GetEnumerator( )
            {
                return GetEnumerator( );
            }
        }
    }
}