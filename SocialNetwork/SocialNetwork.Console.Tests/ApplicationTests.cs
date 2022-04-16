using Moq;
using System;
using Xunit;

namespace SocialNetwork.Console.Tests
{
    public class ApplicationTests
    {
        private readonly Mock<IOutput> _output;
        private readonly Application _application;

        public ApplicationTests()
        {
            _output = new Mock<IOutput>();
            _application = new Application(_output.Object);
        }

        [Fact]
        public void ShouldReturnErrorMessage_IfNoNameIsGiven()
        {
            _application.Run(Array.Empty<string>());

            _output.Verify(x => x.Write("Name is required!"));
        }


        [Fact]
        public void ShouldReturnErrorMessage_IfNameIsWhitespace()
        {
            _application.Run(new string[] {" "});

            _output.Verify(x => x.Write("Name is required!"));
        }

        [Fact]
        public void ShouldReturnErrorMessage_IfInvalidVerbIsGiven()
        {
            _application.Run(new string[] { "Alice /test" });

            _output.Verify(x => x.Write("Invalid verb is given!"));
        }
    }
}