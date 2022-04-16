using Ninject;
using System.Reflection;

namespace SocialNetwork.Console
{
    public class KernelFactory
    {
        public static StandardKernel Create()
        {
            var kernel = new StandardKernel();
            kernel.Load(Assembly.GetExecutingAssembly());
            return kernel;
        }
    }
}
