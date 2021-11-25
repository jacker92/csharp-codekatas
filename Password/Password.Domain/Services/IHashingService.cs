namespace Password.Domain.Services
{
    public interface IHashingService
    {
        string Hash(string value);
        bool VerifyHashedPassword(string hashedPassword, string providedPassword);
    }
}
