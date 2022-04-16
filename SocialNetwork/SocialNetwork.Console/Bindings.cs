using AutoMapper;
using Ninject.Extensions.Conventions;
using Ninject.Modules;
using SocialNetwork.Application;
using SocialNetwork.Console.VerbLogics;
using SocialNetwork.Infrastructure;

namespace SocialNetwork.Console
{
    public class Bindings : NinjectModule
    {
        public override void Load()
        {
            BindWithDefaultConventions();
            BindVerbLogic();
            BindWithoutDefaultConventions();
        }

        private void BindWithDefaultConventions()
        {
            Kernel.Bind(x =>
            {
                x.FromAssembliesMatching($"{nameof(SocialNetwork)}*.dll")
                .SelectAllClasses()
                .WhichAreNotGeneric()
                .BindDefaultInterface();
            });
        }

        private void BindVerbLogic()
        {
            Kernel.Bind(x =>
            {
                x.FromThisAssembly()
                 .SelectAllClasses()
                 .InheritedFrom(typeof(IVerbLogic<>))
                 .BindSingleInterface();
            });
        }

        private void BindWithoutDefaultConventions()
        {
            Bind<IOutput>().To<ConsoleOutput>();
            var dbContext = new AppDbContextFactory().CreateDbContext(null);
            Bind<IApplicationDbContext>().ToConstant(dbContext);
            Bind<IMapper>().ToConstant(new MapperFactory().Create());
        }
    }
}
