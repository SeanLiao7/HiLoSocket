namespace HiLoSocket.CommandFormatter
{
    internal static class FormatterFactory
    {
        internal static ICommandFormatter CreateFormatter( FormatterType formatterType )
        {
            switch ( formatterType )
            {
                case FormatterType.JSonFormatter:
                    return new JsonCommandFormatter( );

                case FormatterType.BinaryFormmater:
                    return new BinaryCommandFormmater( );

                default:
                    return new BinaryCommandFormmater( );
            }
        }
    }
}