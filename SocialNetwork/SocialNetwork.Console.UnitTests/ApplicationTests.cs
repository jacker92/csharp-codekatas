using Moq;
using SocialNetwork.Console.IntegrationTests;
using System;
using Xunit;

namespace SocialNetwork.Console.UnitTests
{
    public class ApplicationTests
    {
        private readonly Application _application;
        private readonly Mock<IOutput> _output;
        private readonly Mock<IVerbLogicRunner> _verbLogicRunner;

        public ApplicationTests()
        {
            _output = new Mock<IOutput>();
            _verbLogicRunner = new Mock<IVerbLogicRunner>();
            _application = new Application(_output.Object, _verbLogicRunner.Object);
        }

        [Fact]
        public void Run_ShouldReturnErrorMessage_IfNullArgumentListIsGiven()
        {
            _application.Run(null);

            _output.Verify(x => x.WriteError("No arguments were given!"));
        }

        [Fact]
        public void Run_ShouldReturnErrorMessage_IfNoNameIsGiven()
        {
            _application.Run(Array.Empty<string>());

            _output.Verify(x => x.WriteError("Name is required!"));
        }

        [Fact]
        public void Run_ShouldReturnErrorMessage_IfNameIsWhitespace()
        {
            _application.Run(new string[] { " " });

            _output.Verify(x => x.WriteError("Name is required!"));
        }

        [Theory, AutoMoqData]
        public void Run_ShouldReturnErrorMessage_IfInvalidVerbIsGiven(string user)
        {
            _application.Run(new string[] { user, "/test" });

            _output.Verify(x => x.WriteError(It.Is<string>(x => x.Contains("Verb '/test' is not recognized."))));
        }

        [Theory, AutoMoqData]
        public void Run_ShouldWriteError_IfApplicationThrowsError(string message, string user)
        {
            _verbLogicRunner.Setup(x => x.Run(It.IsAny<object>(), It.IsAny<string>()))
                .Throws(new Exception(message));

            _application.Run(new string[] { user, "/post", message });

            _output.Verify(x => x.WriteError(message));
        }

        [Theory, AutoMoqData]
        public void Run_ShouldWriteHelp_IfHelpIsRequested(string user)
        {
            _application.Run(new string[] { user, "/post", "--help" });

            _output.Verify(x => x.WriteLine(It.Is<string>(x => x.Contains("--help              Display this help screen."))));
        }
    }
}