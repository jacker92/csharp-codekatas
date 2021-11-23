using System;
using System.ComponentModel.DataAnnotations;
using Xunit;

namespace Password.Domain.Tests
{
    public class UserRepositoryTests
    {
        private readonly UserRepository _userRepository;

        public UserRepositoryTests()
        {
            _userRepository = new UserRepository();
        }

        [Fact]
        public void Add_ShouldThrowArgumentNullException_WithNullUser()
        {
           Assert.Throws<ArgumentNullException>(() => _userRepository.Add(null));
        }

        [Fact]
        public void Add_ShouldThrowValidationException_WithInvalidUser_MissingUserName()
        {
            var user = new User();

            var exception = Assert.Throws<ValidationException>(() => _userRepository.Add(user));
            Assert.Equal($"The {nameof(User.UserName)} field is required.", exception.Message);
        }

        [Fact]
        public void Add_ShouldThrowValidationException_WithInvalidUser_MissingPassword()
        {
            var user = new User
            {
                UserName = "jaakko"
            };

            var exception = Assert.Throws<ValidationException>(() => _userRepository.Add(user));
            Assert.Equal($"The {nameof(User.Password)} field is required.", exception.Message);
        }

        [Fact]
        public void Add_ShouldWork_WithValidUser()
        {
            var user = new User
            {
                UserName = "jaakko",
                Password = "password"
            };

            _userRepository.Add(user);
            Assert.Equal(1, _userRepository.UserCount);
        }

        [Fact]
        public void GetByUserName_ThrowArgumentException_WithEmptyUserName()
        {
            Assert.Throws<ArgumentException>(() => _userRepository.GetByUserName(""));
        }

        [Fact]
        public void GetByUserName_ShouldReturnNull_WhenUserIsNotFound()
        {
            var result =_userRepository.GetByUserName("jaakko");
            Assert.Null(result);
        }

        [Fact]
        public void GetByUserName_ShouldUser_WhenUserIsFound()
        {
            var user = new User
            {
                UserName = "jaakko",
                Password = "password"
            };

            _userRepository.Add(user);

            var result = _userRepository.GetByUserName(user.UserName);
            Assert.Equal(user, result);
        }
    }
}
