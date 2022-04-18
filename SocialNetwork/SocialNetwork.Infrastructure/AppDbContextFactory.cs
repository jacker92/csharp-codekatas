using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace SocialNetwork.Infrastructure
{
    public class AppDbContextFactory : IDesignTimeDbContextFactory<AppDbContext>
    {
        public AppDbContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<AppDbContext>();
            optionsBuilder.UseSqlServer($"Server=(localdb)\\mssqllocaldb;Database=SocialNetwork;Trusted_Connection=True;");

            return new AppDbContext(optionsBuilder.Options);
        }
    }
}