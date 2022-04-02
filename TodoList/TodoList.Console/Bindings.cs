using Ninject.Modules;
using TodoList.Console.CommandLineOptions;
using TodoList.Console.VerbLogics;
using TodoList.Domain;

namespace TodoList.Console
{
    public class Bindings : NinjectModule
    {
        public override void Load()
        {
            Bind<ITodoList>().To<Domain.TodoList>();
            Bind<IOutput>().To<Output>();
            Bind<IVerbLogic<AddOptions>>().To<AddVerbLogic>();
            Bind<IVerbLogic<GetAllOptions>>().To<GetAllLogic>();
        }
    }
}