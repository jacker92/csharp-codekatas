using Password.Domain.Models;

namespace Password.Domain.Services
{
    public interface ITokenService
    {
        Token GeneratePasswordExpirationToken(string email);
    }
}
