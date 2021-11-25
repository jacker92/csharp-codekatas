using Password.Domain.Repositories;
using System;

namespace Password.Domain.Services
{
    public class UserService
    {
        private readonly IUserRepository _userRepository;
        private readonly IHashingService _hasher;
        private readonly IEmailService _emailService;

        public UserService(IUserRepository userRepository, IHashingService hasher, IEmailService emailService)
        {
            _userRepository = userRepository ?? throw new ArgumentNullException(nameof(userRepository));
            _hasher = hasher ?? throw new ArgumentNullException(nameof(hasher));
            _emailService = emailService ?? throw new ArgumentNullException(nameof(emailService));
        }

        public bool AreValidUserCredentials(string userName, string password)
        {
            if (string.IsNullOrWhiteSpace(userName))
            {
                throw new ArgumentException($"'{nameof(userName)}' cannot be null or whitespace.", nameof(userName));
            }

            if (string.IsNullOrWhiteSpace(password))
            {
                throw new ArgumentException($"'{nameof(password)}' cannot be null or whitespace.", nameof(password));
            }

            var user = _userRepository.GetByUserName(userName);

            if (user == null)
            {
                return false;
            }

            var res = _hasher.VerifyHashedPassword(user.Password, password);

            return user != null && res;
        }

        public void SendResetEmail(string email)
        {
            if (string.IsNullOrWhiteSpace(email))
            {
                throw new ArgumentException($"'{nameof(email)}' cannot be null or whitespace.", nameof(email));
            }

            _emailService.SendEmail(email);
        }
    }
}
