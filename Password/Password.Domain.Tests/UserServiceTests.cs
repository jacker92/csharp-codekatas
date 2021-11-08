using Moq;
using System;
using Xunit;

namespace Password.Domain.Tests
{
    public class UserServiceTests
    {
        private Mock<IUserRepository> _userRepository;
        private readonly UserService _userService;

        public UserServiceTests()
        {
            _userRepository = new Mock<IUserRepository>();
            _userService = new UserService(_userRepository.Object);
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
            var user = new User { UserName = "test", Password = "test" };

            _userRepository.Setup(x => x.GetByCredentials("test", "test"))
                .Returns(user);

            var result = _userService.AreValidUserCredentials("test", "test");
            Assert.True(result);
        }
    }
}
