using Moq;
using Xunit;

namespace TodoList.Console.Tests
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
        public void Run_ShouldReturnInvalidArgumentsText_IfPassedInvalidArguments()
        {
            _application.Run(null);

            _output.Verify(x => x.WriteLine(Messages.InvalidArguments));
        }

        [Fact]
        public void Run_ShouldReturnInstructions_IfQuestionMarkIsGiven()
        {
            _application.Run(new string[] {"?"});

            _output.Verify(x => x.WriteLine(Messages.Instructions));
        }

    }
}