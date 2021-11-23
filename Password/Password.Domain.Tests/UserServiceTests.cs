using Moq;
using System;
using Xunit;

namespace Password.Domain.Tests
{
    public class UserServiceTests
    {
        private Mock<IUserRepository> _userRepository;
        private Mock<IHasher> _hasher;
        private readonly UserService _userService;

        public UserServiceTests()
        {
            _userRepository = new Mock<IUserRepository>();
            _hasher = new Mock<IHasher>();
            _userService = new UserService(_userRepository.Object, _hasher.Object);
        }

        [Fact]
        public void AreValidUserCredentials_ShouldThrowArgumentException_WithEmptyUserName()
        {
            Assert.Throws<ArgumentException>(() => _userService.AreValidUserCredentials("", "test"));
        }

        [Fact]
        public void AreValidUserCredentials_ShouldThrowArgumentException_WithEmptyPassword()
        {
            Assert.Throws<ArgumentException>(() => _userService.AreValidUserCredentials("test", ""));
        }

        [Fact]
        public void AreValidUserCredentials_ShouldReturnFalse_IfUserIsNotFound()
        {
            var result = _userService.AreValidUserCredentials("test", "test");
            Assert.False(result);
        }

        [Fact]
        public void AreValidUserCredentials_ShouldReturnTrue_IfUserIsFound()
        {
            var password = "password";
            var hashedPassword = "#Hashed value#"; 

            var user = new User { UserName = "test", Password = hashedPassword };

            _userRepository.Setup(x => x.GetByUserName(user.UserName))
                .Returns(user);

            _hasher.Setup(x => x.Hash(password)).Returns(hashedPassword);

            var result = _userService.AreValidUserCredentials("test", password);
            Assert.True(result);
        }
    }
}
