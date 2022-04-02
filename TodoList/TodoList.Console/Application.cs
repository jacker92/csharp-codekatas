using CommandLine;
using CommandLine.Text;
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

            var parser = new Parser();
            var parserResult = parser.ParseArguments(args ?? Array.Empty<string>(), types);

            parserResult
                 .WithParsed(Run)
                 .WithNotParsed(errors => HandleErrors(errors, parserResult));
        }

        private void HandleErrors(IEnumerable<Error> errors, ParserResult<object> parserResult)
        {
            var result = GenerateHelpText(parserResult);

            if (errors.IsHelp() || errors.IsVersion())
            {
                _output.WriteLine(result);
                return;
            }

            _output.WriteError(result.ToString());
        }

        private string GenerateHelpText(ParserResult<object> parserResult)
        {
            var res = HelpText.AutoBuild(parserResult, h =>
            {
                h.AddEnumValuesToHelpText = true;
                return HelpText.DefaultParsingErrorsHandler(parserResult, h);
            }, e => e);

            return res.ToString();
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