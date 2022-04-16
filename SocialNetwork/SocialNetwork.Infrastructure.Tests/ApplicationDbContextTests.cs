using Microsoft.EntityFrameworkCore;
using Xunit;

namespace SocialNetwork.Infrastructure.Tests
{
    public class ApplicationDbContextTests
    {
        [Fact]
        public void Constructor_ShouldCreateValidDbPath()
        {
           var dbOptions = new DbContextOptionsBuilder<ApplicationDbContext>()
               .UseInMemoryDatabase(databaseName: "Integrat")
               .Options;

            var context = new ApplicationDbContext();
            Assert.NotEmpty(context.DbPath);
        }
    }
}