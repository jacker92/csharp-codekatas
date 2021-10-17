using BalancedBrackets.Domain.Configurations;
using BalancedBrackets.Domain.Models;
using Microsoft.Extensions.Configuration;
using System.IO;

namespace BalancedBrackets.Domain
{
    public class BracketResultConverter : IBracketResultConverter
    {
        private readonly IConfiguration _configuration;

        public BracketResultConverter(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public BracketResultConverter()
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile(Path.Combine(nameof(Configurations), "configurations.json"), optional: false);

            IConfiguration config = builder.Build();

            _configuration = config;
        }

        public string ConvertBracketParsingResult(BracketParsingResult bracketParsingResult)
        {
            var configurations = _configuration.GetSection("BracketParsingResults").Get<BracketParsingResultMapping>();

            return configurations.ResultMap[((int)bracketParsingResult).ToString()];
        }
    }
}
