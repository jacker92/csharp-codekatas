using System;
using System.Collections.Generic;
using System.Linq;

namespace Password.Domain
{
    public class UserRepository : IUserRepository
    {
        private readonly IList<User> _users = new List<User>();

        public int UserCount => _users.Count;

        public void Add(User user)
        {
            if (user is null)
            {
                throw new ArgumentNullException(nameof(user));
            }

            ModelValidator.Validate(user);

            _users.Add(user);
        }

        public User GetByCredentials(string username, string password)
        {
            if (string.IsNullOrWhiteSpace(username))
            {
                throw new ArgumentException($"'{nameof(username)}' cannot be null or whitespace.", nameof(username));
            }

            if (string.IsNullOrWhiteSpace(password))
            {
                throw new ArgumentException($"'{nameof(password)}' cannot be null or whitespace.", nameof(password));
            }

            return _users.SingleOrDefault(x => x.UserName == username && x.Password == password);
        }

        public User GetByUserName(string username)
        {
            if (string.IsNullOrWhiteSpace(username))
            {
                throw new ArgumentException($"'{nameof(username)}' cannot be null or whitespace.", nameof(username));
            }

            return _users.SingleOrDefault(x => x.UserName == username);
        }
    }
}
