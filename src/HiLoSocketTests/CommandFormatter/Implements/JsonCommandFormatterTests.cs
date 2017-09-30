﻿using System;
using HiLoSocket.CommandFormatter;
using NUnit.Framework;

namespace HiLoSocketTests.CommandFormatter.Implements
{
    [TestFixture]
    public class JsonCommandFormatterTests
    {
        [Test]
        public void DeserializeNullInputTest( )
        {
            var formatter = FormatterFactory<JsonData>.CreateFormatter( FormatterType.JSonFormatter );
            Assert.Throws(
                typeof( ArgumentNullException ),
                ( ) => formatter.Deserialize( null ) );
        }

        [Test]
        public void DeserializeZeroLengthTest( )
        {
            var formatter = FormatterFactory<JsonData>.CreateFormatter( FormatterType.JSonFormatter );
            var input = new byte[ 0 ];
            Assert.Throws(
                typeof( ArgumentException ),
                ( ) => formatter.Deserialize( input ) );
        }

        [Test]
        public void SerializeNullInputTest( )
        {
            var formatter = FormatterFactory<JsonData>.CreateFormatter( FormatterType.JSonFormatter );
            Assert.Throws(
                typeof( ArgumentNullException ),
                ( ) => formatter.Serialize( null ) );
        }

        [Test]
        public void SerializeObjectTest( )
        {
            var formatter = FormatterFactory<JsonData>.CreateFormatter( FormatterType.JSonFormatter );
            var expected = new JsonData( Guid.NewGuid( ) )
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
            var formatter = FormatterFactory<string>.CreateFormatter( FormatterType.JSonFormatter );
            const string expected = "*測試 Test#_$% ?";
            var serializedResult = formatter.Serialize( expected );
            var actual = formatter.Deserialize( serializedResult );
            Assert.AreEqual( expected, actual );
        }

        public class JsonData : IEquatable<JsonData>
        {
            public int Age { get; set; }
            public Guid Id { get; }
            public string Name { get; set; }

            public JsonData( Guid id )
            {
                Id = id;
            }

            public static bool operator !=( JsonData a, JsonData b )
            {
                return !( a == b );
            }

            public static bool operator ==( JsonData a, JsonData b )
            {
                return Equals( a, b );
            }

            public bool Equals( JsonData other )
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
                    && Equals( ( JsonData ) obj );
            }

            public override int GetHashCode( )
            {
                return Id.GetHashCode( );
            }
        }
    }
}