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
        private readonly SetAsCompleteLogic _setAsCompleteLogic;
        private readonly VerbLogicRunner _verbLogicRunner;
        private readonly Application _application; 

        public ApplicationTests()
        {
            _output = new Mock<IOutput>();
            _todoList = new Mock<ITodoList>();
            _addVerbLogic = new AddVerbLogic(_todoList.Object);
            _getAllLogic = new GetAllLogic(_output.Object, _todoList.Object);
            _setAsCompleteLogic = new SetAsCompleteLogic(_todoList.Object);
            _verbLogicRunner = new VerbLogicRunner(_addVerbLogic, _getAllLogic, _setAsCompleteLogic);
            _application = new Application(_output.Object, _verbLogicRunner);
        }

        [Fact]
        public void Run_ShouldReturnInvalidArgumentsText_IfPassedInvalidArguments()
        {
            _application.Run(null);

            _output.Verify(x => x.WriteError(It.Is<string>(y => y.Contains("ERROR(S):"))));
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

            _output.Verify(x => x.WriteError(It.Is<string>(x => x.Contains(" Option 'a' is unknown."))));
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
        public void Run_ShouldListAllTodoListItems_WithChildTasksUnderParentTasks()
        {
            var todoItem = new TodoItem { Task = "Parent", Date = DateTime.Parse("12-12-2018") };
            var todoItem2 = new TodoItem { Task = "testChild", Date = DateTime.Parse("12-12-2018"), ParentId = todoItem.Id };
            var todoItem3 = new TodoItem { Task = "testChild2", Date = DateTime.Parse("12-12-2018"), ParentId = todoItem.Id };
            var todoItem4 = new TodoItem { Task = "testChild3", Date = DateTime.Parse("12-12-2018") };

            _todoList.Setup(x => x.GetAll())
                    .Returns(new List<TodoItem> { todoItem, todoItem2, todoItem3, todoItem4 });

            _application.Run(new string[] { "list", "-s", "All" });

            _todoList.Verify(x => x.GetAll(), Times.Once);
            var expected =
                @$"Id: {todoItem.Id}
Task: {todoItem.Task}
Due: {todoItem.Date:dd-MM-yyyy}
> Child Task <
Id: {todoItem2.Id}
Task: {todoItem2.Task}
Due: {todoItem2.Date:dd-MM-yyyy}
> Child Task <
Id: {todoItem3.Id}
Task: {todoItem3.Task}
Due: {todoItem3.Date:dd-MM-yyyy}

Id: {todoItem4.Id}
Task: {todoItem4.Task}
Due: {todoItem4.Date:dd-MM-yyyy}
";

            _output.Verify(x => x.WriteLine(expected));
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

        [Fact]
        public void Run_ShouldComplete_WhenAskedToCompleteTodoItem()
        {
            var todoItem = new TodoItem { Task = "test", Date = DateTime.Parse("12-12-2018") };

            _todoList.Setup(x => x.Complete(todoItem.Id));

            _application.Run(new string[] { "-c", todoItem.Id.ToString() });

            _todoList.Verify(x => x.Complete(todoItem.Id), Times.Once);
        }

    }
}