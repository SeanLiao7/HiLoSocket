using HiLoSocket.CommandFormatter;
using NUnit.Framework;

namespace HiLoSocketTests.CommandFormatter
{
    [TestFixture]
    public class FormatterFactoryTests
    {
        [TestCase( FormatterType.BinaryFormatter )]
        [TestCase( FormatterType.JSonFormatter )]
        [TestCase( FormatterType.MessagePackFormatter )]
        [TestCase( FormatterType.ProtobufFormatter )]
        public void CreateFormatterTest( FormatterType formatterType )
        {
            Shouldly.Should.NotThrow(
                ( ) => FormatterFactory<string>.CreateFormatter( formatterType ) );
        }
    }
}