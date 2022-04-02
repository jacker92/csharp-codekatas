using Moq;
using System;
using System.Collections.Generic;
using TodoList.Console.VerbLogics;
using TodoList.Domain;
using Xunit;

namespace TodoList.Console.Tests
{
    public class ApplicationTests
    {
        private readonly Mock<IOutput> _output;
        private readonly Mock<ITodoList> _todoList;
        private readonly AddVerbLogic _addVerbLogic;
        private readonly GetAllLogic _getAllLogic;
        private readonly Application _application;

        public ApplicationTests()
        {
            _output = new Mock<IOutput>();
            _todoList = new Mock<ITodoList>();
            _addVerbLogic = new AddVerbLogic(_todoList.Object);
            _getAllLogic = new GetAllLogic(_output.Object, _todoList.Object);
            _application = new Application(_output.Object, _addVerbLogic, _getAllLogic);
        }

        [Fact]
        public void Run_ShouldReturnInvalidArgumentsText_IfPassedInvalidArguments()
        {
            _application.Run(null);

            _output.Verify(x => x.WriteLine(Messages.InvalidArguments));
        }

        [Fact]
        public void Run_ShouldReturnInstructions_IfDashDashHelpIsGiven()
        {
            _application.Run(new string[] { "--help" });

            _output.Verify(x => x.WriteLine(It.Is<string>(x => x.Contains("© Microsoft Corporation. All rights reserved."))));
        }

        [Fact]
        public void Run_ShouldReturnVersionInfo_IfDashDashVersionIsGiven()
        {
            _application.Run(new string[] { "--version" });

            _output.Verify(x => x.WriteLine(It.Is<string>(x => x.Contains("testhost 16.11.0"))));
        }

        [Fact]
        public void Run_ShouldWriteError_WithInvalidArguments()
        {
            _application.Run(new string[] { "-asdf" });

            _output.Verify(x => x.WriteError(It.Is<string>(x => x.Contains("Verb '-asdf' is not recognized."))));
        }

        [Fact]
        public void Run_ShouldAddItemToTodoList()
        {
            _application.Run(new string[] { "task", "-t", "application", "-d", "12-12-2021" });

            _todoList.Verify(x => x.Add(It.Is<TodoItem>(x => x.Task == "application" && x.Date == DateTime.Parse("12-12-2021"))));
        }

        [Fact]
        public void Run_ShouldNotAddItemToTodoList_IfNameIsMissing()
        {
            _application.Run(new string[] { "task", "-d", "12-12-2021" });

            _todoList.Verify(x => x.Add(It.IsAny<TodoItem>()), Times.Never);
        }

        [Theory]
        [InlineData("App", "12-12-2018")]
        [InlineData("Complete Application", "01-04-2018")]
        public void Run_ShouldListTodoListItems(string taskName, string dueDate)
        {
            var todoItem = new TodoItem { Task = taskName, Date = DateTime.Parse(dueDate) };

            _todoList.Setup(x => x.GetAll())
                    .Returns(new List<TodoItem> { todoItem });

            _application.Run(new string[] { "list" });

            _todoList.Verify(x => x.GetAll(), Times.Once);

            var expected =
@$"Id: {todoItem.Id}
Task: {todoItem.Task}
Due: {todoItem.Date:dd-MM-yyyy}
";

            _output.Verify(x => x.WriteLine(expected), Times.Once);
        }

        [Fact]
        public void Run_ShouldListAllTodoListItems_WhenAskedWithAllStatus()
        {
            var todoItem = new TodoItem { Task = "test", Date = DateTime.Parse("12-12-2018") };

            _todoList.Setup(x => x.GetAll())
                    .Returns(new List<TodoItem> { todoItem });

            _application.Run(new string[] { "list", "-s", "All" });

            _todoList.Verify(x => x.GetAll(), Times.Once);
        }

        [Fact]
        public void Run_ShouldListAllTodoListItems_WhenAskedWithIncompleteStatus()
        {
            var todoItem = new TodoItem { Task = "test", Date = DateTime.Parse("12-12-2018") };

            _todoList.Setup(x => x.GetAll())
                    .Returns(new List<TodoItem> { todoItem });

            _application.Run(new string[] { "list", "-s", "Incomplete" });

            _todoList.Verify(x => x.GetAll(TodoItemStatus.Incomplete), Times.Once);
        }

    }
}