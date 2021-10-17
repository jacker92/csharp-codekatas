using Autofac;
using System.Diagnostics.CodeAnalysis;
using System.Linq;

namespace HeavyMetalBakeSale.Console
{
    public class Program
    {
        [ExcludeFromCodeCoverage]
        public static void Main(string[] args)
        {
            var container = Container.Build();
            var application = container.Resolve<IApplication>();

            if (args.FirstOrDefault() != null)
            {
                var runOnlyOnce = bool.Parse(args[0]);
                application.Start(runOnlyOnce);
                return;
            }

            application.Start(true);
        }
    }

}
