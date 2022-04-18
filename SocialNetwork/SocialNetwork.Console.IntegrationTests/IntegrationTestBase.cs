﻿using Microsoft.EntityFrameworkCore;
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

            optionsBuilder.UseSqlServer(connectionString ??
              $"Server=(localdb)\\mssqllocaldb;Database=EFSample.{Guid.NewGuid()};Trusted_Connection=True;");

            _appDbContext = new AppDbContext(optionsBuilder.Options);

            _userRepository = new UserRepository(_appDbContext);
            _postRepository = new PostRepository(_appDbContext);
            _directMessageRepository = new DirectMessageRepository(_appDbContext);
            _subscriptionRepository = new SubscriptionRepository(_appDbContext);

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
