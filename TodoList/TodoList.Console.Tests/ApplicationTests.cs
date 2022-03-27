using Moq;
using Xunit;

namespace TodoList.Console.Tests
{
    public class ApplicationTests
    {
        private readonly Mock<IOutput> _output;

        public ApplicationTests()
        {
            _output = new Mock<IOutput>();
        }

        [Fact]
        public void Run_ShouldReturnInvalidArgumentsText_IfPassedInvalidArguments()
        {
            var application = new Application(_output.Object);
            application.Run(null);

            _output.Verify(x => x.WriteLine("Invalid arguments, please enter ? for instructions."));
        }
    }
}