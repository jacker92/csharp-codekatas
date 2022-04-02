using Ninject;
using System.Reflection;

namespace TodoList.Console
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var kernel = CreateKernel();

            var application = kernel.Get<Application>();

            application.Run(args);
        }

        private static StandardKernel CreateKernel()
        {
            var kernel = new StandardKernel();

            kernel.Load(Assembly.GetExecutingAssembly());
            return kernel;
        }
    }
}