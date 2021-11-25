using Password.Domain.Models;
using Password.Domain.Utils;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Password.Domain.Repositories
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
