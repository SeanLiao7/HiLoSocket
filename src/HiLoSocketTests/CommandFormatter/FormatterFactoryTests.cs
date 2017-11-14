using System.Collections;
using System.Collections.Generic;
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

        private class FormatterTypeSource : IEnumerable<FormatterType>
        {
            public IEnumerator<FormatterType> GetEnumerator( )
            {
                yield return FormatterType.BinaryFormatter;
                yield return FormatterType.JSonFormatter;
                yield return FormatterType.MessagePackFormatter;
                yield return FormatterType.ProtobufFormatter;
            }

            IEnumerator IEnumerable.GetEnumerator( )
            {
                return GetEnumerator( );
            }
        }
    }
}