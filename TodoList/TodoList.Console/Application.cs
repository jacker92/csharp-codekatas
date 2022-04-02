using CommandLine;
using System.Reflection;
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
            var types = LoadVerbs();

            var stringWriter = new StringWriter();
            var parser = new Parser(config => config.HelpWriter = stringWriter);

            var arguments = parser.ParseArguments(args, types)
                .WithParsed(Run)
                .WithNotParsed(errors => HandleErrors(stringWriter, errors));
        }

        private void HandleErrors(StringWriter stringWriter, IEnumerable<Error> errors)
        {
            if (errors.IsHelp() || errors.IsVersion())
            {
                _output.WriteLine(stringWriter.ToString());
            }
            else
            {
                _output.WriteError(stringWriter.ToString());
            }
        }

        private static Type[] LoadVerbs()
        {
            return Assembly.GetExecutingAssembly().GetTypes()
                .Where(t => t.GetCustomAttribute<VerbAttribute>() != null).ToArray();
        }

        private void Run(object obj)
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