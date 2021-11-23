namespace Password.Domain
{
    public interface IHashingService
    {
        string Hash(string value);
        bool VerifyHashedPassword(string hashedPassword, string providedPassword);
    }
}
