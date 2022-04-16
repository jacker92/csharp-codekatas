using Ninject;
using SocialNetwork.Application;
using SocialNetwork.Domain;
using SocialNetwork.Infrastructure;
using System;
using System.Reflection;

namespace SocialNetwork.Console
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
