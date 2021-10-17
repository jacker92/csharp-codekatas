using HeavyMetalBakeSale.Domain.Exceptions;
using HeavyMetalBakeSale.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace HeavyMetalBakeSale.Domain.Services
{
    public class SalesCalculationRequestParserService : ISalesCalculationRequestParserService
    {
        public IList<ParsedSalesTotalCalculationRequestResult> Parse(SalesTotalCalculationRequest calculationRequest)
        {
            if (calculationRequest is null)
            {
                throw new ArgumentNullException(nameof(calculationRequest));
            }

            var values = calculationRequest.Request.Split(',');
            var processedRequest = values.Select(x => x.Trim())
                                                     .ToList();

            var result = processedRequest.GroupBy(s => s)
                  .Select(g => new ParsedSalesTotalCalculationRequestResult { Abbreviation = g.Key, Amount = g.Count() });

            return result.ToList();
        }
    }
}
