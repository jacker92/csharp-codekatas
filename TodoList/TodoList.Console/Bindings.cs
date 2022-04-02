using Ninject.Modules;
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
            Bind<IAddVerbLogic>().To<AddVerbLogic>();
            Bind<IGetAllLogic>().To<GetAllLogic>();
        }
    }
}