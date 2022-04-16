﻿using Microsoft.EntityFrameworkCore;
using SocialNetwork.Domain;
using System.Diagnostics.CodeAnalysis;

namespace SocialNetwork.Infrastructure
{
    public class AppDbContext : DbContext, IApplicationDbContext
    {
        public DbSet<Post> Posts { get; set; }
        public DbSet<User> Users { get; set; }

        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>()
                .HasKey(c => c.Id);

            modelBuilder.Entity<Post>()
            .HasKey(c => c.Id);
        }
    }
}