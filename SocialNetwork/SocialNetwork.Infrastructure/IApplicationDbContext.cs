using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using SocialNetwork.Domain;

namespace SocialNetwork.Infrastructure
{
    public interface IApplicationDbContext
    {
        DbSet<Post> Posts { get; set; }
        DbSet<User> Users { get; set; }
        int SaveChanges();
        EntityEntry Entry(object obj);
    }
}