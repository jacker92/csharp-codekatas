using CommandLine;
using System.Reflection;

namespace TodoList.Console
{
    public class Application
    {
        private readonly IOutput _output;
        private readonly IVerbLogicRunner _verbLogicRunner;

        public Application(IOutput output, IVerbLogicRunner verbLogicRunner)
        {
            _output = output;
            _verbLogicRunner = verbLogicRunner;
        }

        public void Run(string[] args)
        {
            var types = LoadVerbs();

            var stringWriter = new StringWriter();
            var parser = new Parser(config => config.HelpWriter = stringWriter);

            parser.ParseArguments(args ?? Array.Empty<string>(), types)
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
            try
            {
                _verbLogicRunner.Run(obj);
            }
            catch (Exception e)
            {
                _output.WriteError(e.Message);
            }
        }
    }
}