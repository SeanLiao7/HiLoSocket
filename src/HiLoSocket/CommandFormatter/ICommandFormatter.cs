namespace HiLoSocket.CommandFormatter
{
    /// <summary>
    /// ICommandFormatter.
    /// </summary>
    /// <typeparam name="TCommandModel">The type of the command model.</typeparam>
    public interface ICommandFormatter<TCommandModel>
        where TCommandModel : class
    {
        /// <summary>
        /// Deserializes the specified bytes.
        /// </summary>
        /// <param name="bytes">The bytes.</param>
        /// <returns>TCommandModel.</returns>
        TCommandModel Deserialize( byte[ ] bytes );

        /// <summary>
        /// Serializes the specified command model.
        /// </summary>
        /// <param name="commandModel">The command model.</param>
        /// <returns>Byte Array.</returns>
        byte[ ] Serialize( TCommandModel commandModel );
    }
}