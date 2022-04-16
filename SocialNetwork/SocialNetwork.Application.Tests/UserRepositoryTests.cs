using SocialNetwork.Application.Repositories;
using SocialNetwork.Console.Tests;
using SocialNetwork.Infrastructure;
using System.Linq;
using Xunit;

namespace SocialNetwork.Application.Tests
{
    public class UserRepositoryTests
    {
        private readonly UserRepository _userRepository;
        private readonly AppDbContext _applicationDbContext;
        public UserRepositoryTests()
        {
            _applicationDbContext = new AppDbContextFactory().CreateInMemoryDbContext();
            _userRepository = new UserRepository(_applicationDbContext);
        }

        [Theory, AutoMoqData]
        public void CreateIfNotExists_ShouldCreateUser(string userName)
        {
            _userRepository.CreateIfNotExists(userName);

            var user = _applicationDbContext.Users.Single();
            Assert.Equal(userName, user.Name);
        }

        [Theory, AutoMoqData]
        public void Update_ShouldUpdateUser(string userName, string userNameToUpdate)
        {
            var user = _userRepository.CreateIfNotExists(userName);
            user.Name = userNameToUpdate;
            _userRepository.Update(user);

            var updatedUser = _applicationDbContext.Users.Single();
            Assert.Equal(userNameToUpdate, updatedUser.Name);
        }
    }
}