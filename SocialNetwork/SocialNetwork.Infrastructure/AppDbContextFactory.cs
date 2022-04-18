using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using System.Linq;

namespace SocialNetwork.Infrastructure
{
    public class AppDbContextFactory : IDesignTimeDbContextFactory<AppDbContext>
    {
        public AppDbContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<AppDbContext>();
            var connectionString = Environment.GetEnvironmentVariable("connection_string");
      
            Console.WriteLine(connectionString);
            optionsBuilder.UseSqlServer(connectionString ?? $"Server=(localdb)\\mssqllocaldb;Database=SocialNetwork;Trusted_Connection=True;");

            Console.WriteLine("Afterr use sql server");
            return new AppDbContext(optionsBuilder.Options);
        }
    }
}