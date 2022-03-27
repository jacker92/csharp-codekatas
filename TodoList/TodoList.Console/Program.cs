using System;

namespace TodoList.Console
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var output = new Output();
            var todolist = new Domain.TodoList();
            var application = new Application(output, todolist);
            application.Run(args);
        }
    }
}