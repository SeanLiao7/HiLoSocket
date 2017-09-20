using System;
using System.Collections.Generic;
using HiLoSocket.Extension;

namespace HiLoSocket.CommandFormatter
{
    public static class FormatterFactory<TCommandModel>
        where TCommandModel : class
    {
        private static readonly Dictionary<FormatterType, ICommandFormatter<TCommandModel>> _formatterTable =
            new Dictionary<FormatterType, ICommandFormatter<TCommandModel>>( );

        /// <summary>
        /// Creates the formatter.
        /// </summary>
        /// <param name="formatterType">Type of the formatter.</param>
        /// <returns>Instance which has implemented ICommandFormatter&lt;TCommandModel&gt;.</returns>
        /// <exception cref="InvalidOperationException">formatter</exception>
        public static ICommandFormatter<TCommandModel> CreateFormatter( FormatterType formatterType )
        {
            if ( _formatterTable.TryGetValue( formatterType, out var formatter ) )
                return formatter;

            var genericFormatter = Type.GetType( $"HiLoSocket.CommandFormatter.Implements.{formatterType.GetDescription( )}`1" );
            var genericForamatterClass = genericFormatter?.MakeGenericType( typeof( TCommandModel ) );
            if ( genericForamatterClass != null )
            {
                formatter = Activator.CreateInstance( genericForamatterClass ) as ICommandFormatter<TCommandModel>;
                _formatterTable.Add( formatterType, formatter );
            }

            if ( formatter == null )
                throw new InvalidOperationException( $"無法建立對應 {nameof( formatter )} 的物件" );

            return formatter;
        }
    }
}