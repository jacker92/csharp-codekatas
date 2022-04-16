using Microsoft.EntityFrameworkCore;
using SocialNetwork.Domain;
using System.Diagnostics.CodeAnalysis;

namespace SocialNetwork.Infrastructure
{
    [ExcludeFromCodeCoverage]
    public class ApplicationDbContext : DbContext, IApplicationDbContext
    {
        public DbSet<Post> Posts { get; set; }
        public DbSet<User> Users { get; set; }

        public string DbPath { get; }

        public ApplicationDbContext()
        {
            var folder = Environment.SpecialFolder.LocalApplicationData;
            var path = Environment.GetFolderPath(folder);
            DbPath = Path.Join(path, "socialnetwork.db");
        }

        protected override void OnConfiguring(DbContextOptionsBuilder options)
            => options.UseSqlite($"Data Source={DbPath}");
    }
}