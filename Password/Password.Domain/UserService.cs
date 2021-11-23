using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Password.Domain
{
    public class UserService
    {
        private readonly IUserRepository _userRepository;
        private readonly IHashingService _hasher;

        public UserService(IUserRepository userRepository, IHashingService hasher)
        {
            _userRepository = userRepository;
            _hasher = hasher;
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

            return user != null && user.Password == _hasher.Hash(password);
        }
    }
}
