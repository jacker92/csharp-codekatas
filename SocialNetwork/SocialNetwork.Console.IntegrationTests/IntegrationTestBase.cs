using Microsoft.EntityFrameworkCore;
using SocialNetwork.Application.Repositories;
using SocialNetwork.Infrastructure;
using System;

namespace SocialNetwork.Console.IntegrationTests
{
    public class IntegrationTestBase : IDisposable
    {
        protected readonly PostRepository _postRepository;
        protected readonly UserRepository _userRepository;
        protected readonly DirectMessageRepository _directMessageRepository;
        protected readonly SubscriptionRepository _subscriptionRepository;

        private readonly AppDbContext _appDbContext;

        public IntegrationTestBase()
        {
            var optionsBuilder = new DbContextOptionsBuilder<AppDbContext>();

            var connectionString = Environment.GetEnvironmentVariable("connection_string");

            if (connectionString != null)
            {
                connectionString += $";Initial Catalog=EFSample.{Guid.NewGuid()}";
            }

            // var connectionString = $"Data Source=sql-server-db;Initial Catalog=EFSample.{Guid.NewGuid()};User Id=sa;Password=Guess_me;";


            optionsBuilder.UseSqlServer(connectionString ??
              $"Database=EFSample.{Guid.NewGuid()};Trusted_Connection=True;");

            _appDbContext = new AppDbContext(optionsBuilder.Options);

            _userRepository = new UserRepository(_appDbContext);
            _postRepository = new PostRepository(_appDbContext);
            _directMessageRepository = new DirectMessageRepository(_appDbContext);
            _subscriptionRepository = new SubscriptionRepository(_appDbContext);

            //try
            //{
                _appDbContext.Database.Migrate();
            //}
            //catch (Exception e)
            //{
            //    System.Console.WriteLine("Exception thrown: " + e.Message + ", " + e.StackTrace);
            //    _appDbContext?.Database?.EnsureDeleted();
            //}
        }

        public void Dispose()
        {
            _appDbContext.Database.EnsureDeleted();
        }
    }
}
