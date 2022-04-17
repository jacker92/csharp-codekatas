using SocialNetwork.Application.Repositories;
using SocialNetwork.Console.Tests;
using SocialNetwork.Domain.Models;
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
            var request = new User { Name = userName };
            _userRepository.Create(request);

            var user = _applicationDbContext.Users.Single();
            Assert.Equal(userName, user.Name);
        }

        [Theory, AutoMoqData]
        public void Update_ShouldUpdateUser(string userName, string userNameToUpdate)
        {
            var user = new User { Name = userName };
            _userRepository.Create(user);

            user.Name = userNameToUpdate;

            _userRepository.Update(user);

            var updatedUser = _applicationDbContext.Users.Single();
            Assert.Equal(userNameToUpdate, updatedUser.Name);
        }
    }
}