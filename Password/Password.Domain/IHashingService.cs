namespace Password.Domain
{
    public interface IHashingService
    {
        string Hash(string value);   
    }
}
