using Ninject.Modules;
using TodoList.Console.VerbLogics;
using Ninject.Extensions.Conventions;

namespace TodoList.Console
{
    public class Bindings : NinjectModule
    {
        public override void Load()
        {
            BindWithDefaultConventions();
            BindVerbLogic();
        }

        private void BindWithDefaultConventions()
        {
            Kernel.Bind(x =>
            {
                x.FromAssembliesMatching($"{nameof(TodoList)}*.dll")
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
    }
}