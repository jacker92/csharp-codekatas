using Xunit;

namespace Password.Domain.IntegrationTests
{
    public class UserServiceTests
    {
        [Fact]
        public void AreValidUserCredentials_ShouldWorkCorrectly()
        {
            var userRepository = new UserRepository();
            var hashingService = new HashingService();
            var userService = new UserService(userRepository, hashingService);

            var password = "P@ssw0rd";

            var user = new User { UserName = "jaakko", Password = hashingService.Hash(password) };

            userRepository.Add(user);

            var result = userService.AreValidUserCredentials("jaakko", password);

            Assert.True(result);
        }
    }
}
