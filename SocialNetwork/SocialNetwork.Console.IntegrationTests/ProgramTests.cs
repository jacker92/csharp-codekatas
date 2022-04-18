using System;
using Xunit;

namespace SocialNetwork.Console.Tests
{
    public class ProgramTests
    {
        [Fact]
        public void MainTest()
        {
            Program.Main(Array.Empty<string>());
        }
    }
}
