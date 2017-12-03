using System;
using System.Collections;
using System.Linq;
using HiLoSocket.CommandFormatter;
using NUnit.Framework;

namespace HiLoSocketTests.CommandFormatter
{
    [TestFixture]
    [Category( "FormatterFactoryTests" )]
    public class FormatterFactoryTests
    {
        [TestCaseSource( typeof( FormatterTypeSource ) )]
        public void CreateFormatter_FormatterType_ShouldNotThrowException( FormatterType formatterType )
        {
            Shouldly.Should.NotThrow(
                ( ) => FormatterFactory<string>.CreateFormatter( formatterType ) );
        }

        private class FormatterTypeSource : IEnumerable
        {
            public IEnumerator GetEnumerator( )
            {
                return Enum.GetValues( typeof( FormatterType ) ).Cast<FormatterType>( ).GetEnumerator( );
            }
        }
    }
}