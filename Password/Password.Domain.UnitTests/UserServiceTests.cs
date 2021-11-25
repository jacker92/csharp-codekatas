using Moq;
using System;
using Xunit;

namespace Password.Domain.Tests
{
    public class UserServiceTests
    {
        private Mock<IUserRepository> _userRepository;
        private Mock<IHashingService> _hashingService;
        private Mock<IEmailService> _emailService;
        private readonly UserService _userService;

        public UserServiceTests()
        {
            _userRepository = new Mock<IUserRepository>();
            _hashingService = new Mock<IHashingService>();
            _emailService = new Mock<IEmailService>();
            _userService = new UserService(_userRepository.Object, _hashingService.Object, _emailService.Object);
        }

        [Fact]
        public void ShouldThrowArgumentNullException_WithNullUserRepository()
        {
            Assert.Throws<ArgumentNullException>(() => new UserService(null, _hashingService.Object, _emailService.Object));
        }

        [Fact]
        public void ShouldThrowArgumentNullException_WithNullHashingService()
        {
            Assert.Throws<ArgumentNullException>(() => new UserService(_userRepository.Object, null, _emailService.Object));
        }

        [Fact]
        public void ShouldThrowArgumentNullException_WithNullEmailService()
        {
            Assert.Throws<ArgumentNullException>(() => new UserService(_userRepository.Object, _hashingService.Object, null));
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

            _hashingService.Setup(x => x.Hash(password)).Returns(hashedPassword);
            _hashingService.Setup(x => x.VerifyHashedPassword(hashedPassword, password)).Returns(true);

            var result = _userService.AreValidUserCredentials("test", password);
            Assert.True(result);
        }

        [Fact]
        public void SendResetEmail_ShouldThrowArgumentException_WithEmptyEmail()
        {
            Assert.Throws<ArgumentException>(() => _userService.SendResetEmail(""));
        }

        [Fact]
        public void SendResetEmail_ShouldWork()
        {
            var email = "test@test.fi";

            _userService.SendResetEmail(email);

            _emailService.Verify(x => x.SendEmail(email), Times.Once);
        }
    }
}
