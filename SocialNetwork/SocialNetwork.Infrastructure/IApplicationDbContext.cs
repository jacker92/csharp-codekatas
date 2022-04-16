using Microsoft.EntityFrameworkCore;
using SocialNetwork.Domain;

namespace SocialNetwork.Infrastructure
{
    public interface IApplicationDbContext
    {
        string DbPath { get; }
        DbSet<Post> Posts { get; set; }
        DbSet<User> Users { get; set; }
        int SaveChanges();
    }
}