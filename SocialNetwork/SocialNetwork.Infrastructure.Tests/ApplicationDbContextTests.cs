using Xunit;

namespace SocialNetwork.Infrastructure.Tests
{
    public class ApplicationDbContextTests
    {
        [Fact]
        public void Constructor_ShouldCreateValidDbPath()
        {
            var context = new ApplicationDbContext();
            Assert.NotEmpty(context.DbPath);
        }
    }
}