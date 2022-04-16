using AutoFixture.Xunit2;
using Microsoft.EntityFrameworkCore;
using Moq;
using SocialNetwork.Application.Repositories;
using SocialNetwork.Domain;
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

        [Theory, AutoData]
        public void CreateIfNotExists(string userName)
        {
            _userRepository.CreateIfNotExists(userName);

            var user = _applicationDbContext.Users.Single();
            Assert.Equal(userName, user.Name);
        }
    }
}