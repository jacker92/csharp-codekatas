namespace Password.Domain
{
    public interface IHasher
    {
        string Hash(string value);   
    }
}
