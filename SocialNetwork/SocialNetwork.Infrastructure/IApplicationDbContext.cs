using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using SocialNetwork.Domain.Models;

namespace SocialNetwork.Infrastructure
{
    public interface IApplicationDbContext
    {
        DbSet<Post> Posts { get; set; }
        DbSet<User> Users { get; set; }
        DbSet<DirectMessage> DirectMessages { get; set; }
        int SaveChanges();
        EntityEntry Entry(object obj);
    }
}