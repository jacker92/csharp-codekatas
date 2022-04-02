using CommandLine;
using System.Text;
using TodoList.Console.CommandLineOptions;
using TodoList.Console.VerbLogics;
using TodoList.Domain;

namespace TodoList.Console
{
    public class Application
    {
        private readonly IOutput _output;
        private readonly VerbLogic<AddOptions> _addLogic;
        private readonly VerbLogic<GetAllOptions> _getAllLogic;

        public Application(IOutput output, VerbLogic<AddOptions> addLogic, VerbLogic<GetAllOptions> getAllLogic)
        {
            _output = output;
            _addLogic = addLogic;
            _getAllLogic = getAllLogic;
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
            var arguments = parser.ParseArguments<AddOptions, GetAllOptions>(args)
                .MapResult(
                (AddOptions options) => _addLogic.Run(options),
                (GetAllOptions options) => _getAllLogic.Run(options),
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