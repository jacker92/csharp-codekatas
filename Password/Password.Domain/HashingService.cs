using Microsoft.AspNetCore.Identity;

namespace Password.Domain
{
    public class HashingService : IHashingService
    {
        private readonly PasswordHasher<object?> _passwordHasher;

        public HashingService()
        {
            _passwordHasher = new PasswordHasher<object?>();
        }

        public string Hash(string value)
        {
           return _passwordHasher.HashPassword(null,value);
        }

        public bool VerifyHashedPassword(string hashedPassword, string providedPassword)
        {
            var result = _passwordHasher.VerifyHashedPassword(null,hashedPassword, providedPassword);
            return result == PasswordVerificationResult.Success;
        }
    }
}
