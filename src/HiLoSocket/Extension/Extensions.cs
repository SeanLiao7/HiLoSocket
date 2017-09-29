using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace HiLoSocket.Extension
{
    /// <summary>
    /// Extensions.
    /// </summary>
    public static class Extensions
    {
        /// <summary>
        /// Get dateTime string with specified datetime.
        /// </summary>
        /// <param name="dateTime">DateTime.</param>
        /// <param name="provider">Provider.</param>
        /// <returns>Datetime string.</returns>
        public static string GetDateTimeString( this DateTime dateTime, string provider = "yyyy-MM-dd hh-mm-ss" )
        {
            return dateTime.ToString( provider );
        }

        /// <summary>
        /// Gets the description.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns>Description.</returns>
        public static string GetDescription( this Enum value )
        {
            var fi = value.GetType( ).GetField( value.ToString( ) );
            var attributes = fi.GetCustomAttributes( typeof( DescriptionAttribute ), false ) as DescriptionAttribute[ ];

            return attributes?.Length > 0 ? attributes?[ 0 ].Description : value.ToString( );
        }

        /// <summary>
        /// Validate object.
        /// </summary>
        /// <typeparam name="T">Target type T.</typeparam>
        /// <param name="value">Input value.</param>
        /// <param name="errorMessages">Error messages.</param>
        /// <returns>IsSuccess.</returns>
        public static bool ValidateObject<T>( this T value, out IEnumerable<string> errorMessages )
            where T : class
        {
            var context = new ValidationContext( value, null, null );
            var errors = new List<ValidationResult>( );

            var success = Validator.TryValidateObject( value, context, errors, true );
            errorMessages = errors.Select( x => x.ErrorMessage );
            return success;
        }
    }
}