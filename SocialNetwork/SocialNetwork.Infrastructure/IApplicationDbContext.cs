using Microsoft.EntityFrameworkCore;
using SocialNetwork.Domain.Models;

namespace SocialNetwork.Infrastructure
{
    public interface IApplicationDbContext
    {
        DbSet<Post> Posts { get; set; }
        DbSet<User> Users { get; set; }
        int SaveChanges();
    }
}