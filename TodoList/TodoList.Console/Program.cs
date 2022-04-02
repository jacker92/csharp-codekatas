using System;
using TodoList.Console.VerbLogics;

namespace TodoList.Console
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var output = new Output();
            var todolist = new Domain.TodoList();
            var addLogic = new AddVerbLogic(todolist);
            var getAllLogic = new GetAllLogic(output, todolist);
            var application = new Application(output, addLogic, getAllLogic);
            application.Run(args);
        }
    }
}