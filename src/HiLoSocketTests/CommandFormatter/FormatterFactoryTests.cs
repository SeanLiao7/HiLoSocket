using HiLoSocket.CommandFormatter;
using NUnit.Framework;

namespace HiLoSocketTests.CommandFormatter
{
    [TestFixture]
    [Category( "FormatterFactoryTests" )]
    public class FormatterFactoryTests
    {
        [TestCase( FormatterType.BinaryFormatter )]
        [TestCase( FormatterType.JSonFormatter )]
        [TestCase( FormatterType.MessagePackFormatter )]
        [TestCase( FormatterType.ProtobufFormatter )]
        public void CreateFormatter_FormatterType_ShouldNotThrowException( FormatterType formatterType )
        {
            Shouldly.Should.NotThrow(
                ( ) => FormatterFactory<string>.CreateFormatter( formatterType ) );
        }
    }
}