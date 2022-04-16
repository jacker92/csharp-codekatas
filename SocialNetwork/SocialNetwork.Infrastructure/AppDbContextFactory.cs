using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace SocialNetwork.Infrastructure
{
    public class AppDbContextFactory : IDesignTimeDbContextFactory<AppDbContext>
    {
        public AppDbContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<AppDbContext>();
            var folder = Environment.SpecialFolder.LocalApplicationData;
            var path = Environment.GetFolderPath(folder);
            var joinedPath = Path.Join(path, "socialnetwork.db");
            optionsBuilder.UseSqlite($"Data Source={joinedPath}");

            return new AppDbContext(optionsBuilder.Options);
        }

        public AppDbContext CreateInMemoryDbContext()
        {
            var dbOptions = new DbContextOptionsBuilder<AppDbContext>()
           .UseInMemoryDatabase(Guid.NewGuid().ToString())
           .Options;

            return new AppDbContext(dbOptions);
        }
    }
}