using Moq;
using System;
using Xunit;

namespace SocialNetwork.Console.Tests
{
    public class ApplicationTests
    {
        private readonly Mock<IOutput> _output;
        private readonly Mock<IVerbLogicRunner> _verbLogicRunner;
        private readonly Application _application;

        public ApplicationTests()
        {
            _output = new Mock<IOutput>();
            _verbLogicRunner = new Mock<IVerbLogicRunner>();
            _application = new Application(_output.Object, _verbLogicRunner.Object);
        }

        [Fact]
        public void ShouldReturnErrorMessage_IfNullArgumentListIsGiven()
        {
            _application.Run(null);

            _output.Verify(x => x.WriteError("No arguments were given!"));
        }

        [Fact]
        public void ShouldReturnErrorMessage_IfNoNameIsGiven()
        {
            _application.Run(Array.Empty<string>());

            _output.Verify(x => x.WriteError("Name is required!"));
        }


        [Fact]
        public void ShouldReturnErrorMessage_IfNameIsWhitespace()
        {
            _application.Run(new string[] {" "});

            _output.Verify(x => x.WriteError("Name is required!"));
        }

        [Fact]
        public void ShouldReturnErrorMessage_IfInvalidVerbIsGiven()
        {
            _application.Run(new string[] { "Alice /test" });

            _output.Verify(x => x.WriteError(It.Is<string>(x => x.Contains("No verb selected."))));
        }
    }
}