using SocialNetwork.Application.Mappings;
using SocialNetwork.Application.Repositories;
using SocialNetwork.Console.Tests;
using SocialNetwork.Domain.Requests;
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
            _userRepository = new UserRepository(_applicationDbContext, MapperFactory.Create());
        }

        [Theory, AutoMoqData]
        public void CreateIfNotExists_ShouldCreateUser(string userName)
        {
            var request = new CreateUserRequest { Name = userName };
            _userRepository.CreateIfNotExists(request);

            var user = _applicationDbContext.Users.Single();
            Assert.Equal(userName, user.Name);
        }

        [Theory, AutoMoqData]
        public void Update_ShouldUpdateUser(string userName, string userNameToUpdate)
        {
            var request = new CreateUserRequest { Name = userName };
            var user = _userRepository.CreateIfNotExists(request);

            var updateUserRequest = new UpdateUserRequest
            {
                Id = user.Id,
                Name = userNameToUpdate
            };

            _userRepository.Update(updateUserRequest);

            var updatedUser = _applicationDbContext.Users.Single();
            Assert.Equal(userNameToUpdate, updatedUser.Name);
        }
    }
}