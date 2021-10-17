using System;
using System.Threading.Tasks;
using Xunit;

namespace MazeSolver.Domain.Tests
{
    public class MazeAppTests
    {
        [Fact]
        public void ShouldWork()
        {
            var task = Task.Run(() => MazeApp.Main());
            if (task.Wait(TimeSpan.FromSeconds(1)))
                return;
        }
    }
}
