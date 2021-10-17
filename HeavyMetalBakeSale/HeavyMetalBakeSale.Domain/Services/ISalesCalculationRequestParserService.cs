using HeavyMetalBakeSale.Domain.Models;
using System.Collections.Generic;

namespace HeavyMetalBakeSale.Domain.Services
{
    public interface ISalesCalculationRequestParserService
    {
        IList<ParsedSalesTotalCalculationRequestResult> Parse(SalesTotalCalculationRequest calculationRequest);
    }
}