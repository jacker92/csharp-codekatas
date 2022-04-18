using Microsoft.EntityFrameworkCore;
using SocialNetwork.Application.Repositories;
using SocialNetwork.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocialNetwork.Console.Tests
{
    public class IntegrationTestBase : IDisposable
    {
        protected readonly PostRepository _postRepository;
        protected readonly UserRepository _userRepository;
        protected readonly DirectMessageRepository _directMessageRepository;
        private readonly AppDbContext _appDbContext;

        public IntegrationTestBase()
        {
            var optionsBuilder = new DbContextOptionsBuilder<AppDbContext>();
            optionsBuilder.UseSqlServer(
              $"Server=(localdb)\\mssqllocaldb;Database=EFSample.{Guid.NewGuid()};Trusted_Connection=True;");

            _appDbContext = new AppDbContext(optionsBuilder.Options);

            _userRepository = new UserRepository(_appDbContext);
            _postRepository = new PostRepository(_appDbContext);
            _directMessageRepository = new DirectMessageRepository(_appDbContext);

            try
            {
                _appDbContext.Database.Migrate();
            }
            catch (Exception)
            {
                _appDbContext?.Database?.EnsureDeleted();
            }
        }

        public void Dispose()
        {
            _appDbContext.Database.EnsureDeleted();
        }
    }
}
