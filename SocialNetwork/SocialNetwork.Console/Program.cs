using Ninject;
using SocialNetwork.Application;
using SocialNetwork.Domain;
using SocialNetwork.Infrastructure;
using System;

namespace SocialNetwork.Console
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var kernel = KernelFactory.Create();

            var application = kernel.Get<Application>();

            application.Run(args);
        }
    }
}
