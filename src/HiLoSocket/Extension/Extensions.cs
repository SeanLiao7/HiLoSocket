using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace HiLoSocket.Extension
{
    public static class Extensions
    {
        public static string GetDateTimeString( this DateTime dateTime, string provider = "yyyy-MM-dd hh-mm-ss" )
        {
            return dateTime.ToString( provider );
        }

        public static string GetDescription<T>( this T value )
        {
            var fi = value.GetType( ).GetField( value.ToString( ) );
            var attributes = fi.GetCustomAttributes( typeof( DescriptionAttribute ), false ) as DescriptionAttribute[ ];

            return attributes?.Length > 0 ? attributes?[ 0 ].Description : value.ToString( );
        }

        public static bool ValidateObject<T>( this T value, out IEnumerable<string> errorMessages )
        {
            var context = new ValidationContext( value, null, null );
            var errors = new List<ValidationResult>( );

            var success = Validator.TryValidateObject( value, context, errors, true );
            errorMessages = errors.Select( x => x.ErrorMessage );
            return success;
        }
    }
}