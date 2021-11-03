using System;
using Xunit;

namespace Password.Domain.Tests
{
    public class UserServiceTests
    {
        private readonly UserService _userService;

        public UserServiceTests()
        {
            _userService = new UserService();   
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

        //[Fact]
        //public void AreValidUserCredentials_ShouldReturnFalse_IfUserIsNotFound()
        //{
        //    Assert.Throws<ArgumentException>(() => _userService.AreValidUserCredentials("test", "test"));
        //}
    }
}
