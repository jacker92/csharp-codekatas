using Ninject;

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
