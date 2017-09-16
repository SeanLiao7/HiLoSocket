namespace HiLoSocket.CommandFormatter
{
    public interface ICommandFormatter
    {
        T Deserialize<T>( byte[ ] bytes ) where T : class;

        byte[ ] Serialize<T>( T command ) where T : class;
    }
}