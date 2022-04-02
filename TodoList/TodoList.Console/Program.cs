using Ninject;
using System;
using System.Reflection;

namespace TodoList.Console
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var kernel = new StandardKernel();
            kernel.Load(Assembly.GetExecutingAssembly());

            var application = kernel.Get<Application>();

            application.Run(args);
        }
    }
}