using Microsoft.EntityFrameworkCore;
using SocialNetwork.Domain.Models;

namespace SocialNetwork.Infrastructure
{
    public class AppDbContext : DbContext, IApplicationDbContext
    {
        public DbSet<Post> Posts { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<DirectMessage> DirectMessages { get; set; }

        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Subscription>()
                .HasKey(x => x.Id);

            modelBuilder.Entity<User>()
                .HasKey(c => c.Id);

            modelBuilder.Entity<User>()
             .HasMany(x => x.Subscriptions)
             .WithOne()
             .HasForeignKey(x => x.SubscriberId);

            modelBuilder.Entity<User>()
             .HasMany(x => x.Subscriptions)
             .WithOne()
             .HasForeignKey(x => x.SubscribedId);

            modelBuilder.Entity<Post>()
                 .HasKey(c => c.Id);

            modelBuilder.Entity<DirectMessage>()
                .HasKey(c => c.Id);

            foreach (var relationship in modelBuilder.Model.GetEntityTypes().SelectMany(e => e.GetForeignKeys()))
            {
                relationship.DeleteBehavior = DeleteBehavior.Restrict;
            }
        }
    }
}