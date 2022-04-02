using CommandLine;
using TodoList.Console.CommandLineOptions;
using TodoList.Console.VerbLogics;

namespace TodoList.Console
{
    public class Application
    {
        private readonly IOutput _output;
        private readonly IVerbLogic<AddOptions> _addLogic;
        private readonly IVerbLogic<GetAllOptions> _getAllLogic;
        private readonly IVerbLogic<SetAsCompleteOptions> _setAsCompleteLogic;

        public Application(IOutput output, IVerbLogic<AddOptions> addLogic, IVerbLogic<GetAllOptions> getAllLogic, IVerbLogic<SetAsCompleteOptions> setAsCompleteLogic)
        {
            _output = output;
            _addLogic = addLogic;
            _getAllLogic = getAllLogic;
            _setAsCompleteLogic = setAsCompleteLogic;
        }

        public void Run(string[] args)
        {
            if (args is null)
            {
                _output.WriteLine(Messages.InvalidArguments);
                return;
            }

            ParseArgumentsAndInvoke(args);
        }

        private void ParseArgumentsAndInvoke(string[] args)
        {
            var stringWriter = new StringWriter();
            var parser = new Parser(config => config.HelpWriter = stringWriter);
            var arguments = parser.ParseArguments<AddOptions, GetAllOptions, SetAsCompleteOptions>(args)
                .MapResult(
                (AddOptions options) => _addLogic.Run(options),
                (GetAllOptions options) => _getAllLogic.Run(options),
                (SetAsCompleteOptions options) => _setAsCompleteLogic.Run(options),
                errors => HandleError(stringWriter, errors));
        }

        private int HandleError(StringWriter stringWriter, IEnumerable<Error> error)
        {
            if (error.IsHelp() || error.IsVersion())
            {
                _output.WriteLine(stringWriter.ToString());
            }
            else
            {
                _output.WriteError(stringWriter.ToString());
            }

            return (int)ApplicationExitCode.Error;
        }
    }
}