using CommandLine;
using CommandLine.Text;
using SocialNetwork.Console.CommandLineOptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace SocialNetwork.Console
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
            if (args == null)
            {
                _output.WriteError("No arguments were given!");
                return;
            }

            var name = args.FirstOrDefault();

            if (string.IsNullOrWhiteSpace(name))
            {
                _output.WriteError("Name is required!");
                return;
            }

            var types = LoadVerbs();

            var parser = new Parser();
            var parserResult = parser.ParseArguments(args.Skip(1), types);

            parserResult
                 .WithParsed(x => Run(x, name))
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

        private void Run(object options, string userName)
        {
            try
            {
                _verbLogicRunner.Run(options, userName);
            }
            catch (Exception e)
            {
                _output.WriteError(e.Message);
            }
        }
    }
}
