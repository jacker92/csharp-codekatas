using System;
using System.Collections.Generic;

namespace Password.Domain
{
    public class UserRepository
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
    }
}
