using Moq;
using System;
using TodoList.Domain;
using Xunit;

namespace TodoList.Console.Tests
{
    public class ApplicationTests
    {
        private readonly Mock<IOutput> _output;
        private readonly Mock<ITodoList> _todoList;
        private readonly Application _application;

        public ApplicationTests()
        {
            _output = new Mock<IOutput>();
            _todoList = new Mock<ITodoList>();
            _application = new Application(_output.Object, _todoList.Object);
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

        [Fact]
        public void Run_ShouldAddItemToTodoList()
        {
            _application.Run(new string[] { "task", "-t", "application", "-d", "12-12-2021" });

            _todoList.Verify(x => x.Add(It.Is<TodoItem>(x => x.Task == "application" && x.Date == DateTime.Parse("12-12-2021"))));
        }

    }
}