using System;
using System.Collections.Generic;

namespace HiLoSocket.CommandFormatter
{
    public static class FormatterFactory
    {
        private static readonly Dictionary<FormatterType, ICommandFormatter> _formatterTable =
            new Dictionary<FormatterType, ICommandFormatter>( );

        public static ICommandFormatter CreateFormatter( FormatterType formatterType )
        {
            if ( _formatterTable.TryGetValue( formatterType, out var formatter ) )
                return formatter;

            var type = Type.GetType( $"HiLoSocket.CommandFormatter.Implements.{formatterType.GetDescription( )}" );
            formatter = ( ICommandFormatter ) Activator.CreateInstance(
                type ?? throw new InvalidOperationException( nameof( formatterType ) ) );

            _formatterTable.Add( formatterType, formatter );

            return formatter;
        }
    }
}