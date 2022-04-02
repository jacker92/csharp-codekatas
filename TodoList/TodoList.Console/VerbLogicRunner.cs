using TodoList.Console.CommandLineOptions;
using TodoList.Console.VerbLogics;

namespace TodoList.Console
{
    public class VerbLogicRunner : IVerbLogicRunner
    {
        private readonly IVerbLogic<AddOptions> _addLogic;
        private readonly IVerbLogic<GetAllOptions> _getAllLogic;
        private readonly IVerbLogic<SetAsCompleteOptions> _setAsCompleteLogic;

        public VerbLogicRunner(IVerbLogic<AddOptions> addLogic, IVerbLogic<GetAllOptions> getAllLogic, IVerbLogic<SetAsCompleteOptions> setAsCompleteLogic)
        {
            _addLogic = addLogic;
            _getAllLogic = getAllLogic;
            _setAsCompleteLogic = setAsCompleteLogic;
        }

        public void Run(object obj)
        {
            switch (obj)
            {
                case AddOptions a:
                    _addLogic.Run(a);
                    break;
                case GetAllOptions g:
                    _getAllLogic.Run(g);
                    break;
                case SetAsCompleteOptions s:
                    _setAsCompleteLogic.Run(s);
                    break;
            }
        }
    }
}