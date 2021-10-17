using HeavyMetalBakeSale.Domain.Exceptions;
using HeavyMetalBakeSale.Domain.Models;
using HeavyMetalBakeSale.Domain.Services;
using Moq;
using System;
using System.Linq;
using Xunit;

namespace HeavyMetalBakeSale.Domain.Tests
{
    public class SalesCalculationRequestParserServiceTests
    {
        private readonly SalesCalculationRequestParserService _salesCalculationRequestParserService;

        public SalesCalculationRequestParserServiceTests()
        {
            _salesCalculationRequestParserService = new SalesCalculationRequestParserService();
        }

        [Fact]
        public void Parse_ShouldThrowArgumentNullException_WithNulleRequest()
        {
            Assert.Throws<ArgumentNullException>(() => _salesCalculationRequestParserService.Parse(null));
        }

        [Theory]
        [InlineData("B", "M", "B,M")]
        public void Parse_ShouldParseValidRequest(string item1, string item2, string requestString)
        {
            var request = new SalesTotalCalculationRequest { Request = requestString };

            var result = _salesCalculationRequestParserService.Parse(request);

            Assert.NotNull(result);
            Assert.Equal(2, result.Count());
            Assert.Equal(item1, result[0].Abbreviation);
            Assert.Equal(item2, result[1].Abbreviation);
        }
    }
}
