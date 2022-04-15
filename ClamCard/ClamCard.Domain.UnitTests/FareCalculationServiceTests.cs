using ClamCard.Domain.Models;
using ClamCard.Domain.Services;
using Moq;
using Xunit;

namespace ClamCard.Domain.UnitTests
{
    public class FareCalculationServiceTests
    {
        private readonly FareCalculationService _fareCalculationService;
        private readonly Mock<IJourneyFareCalculationService> _journeyFareCalculationService;

        public FareCalculationServiceTests()
        {
            _journeyFareCalculationService = new Mock<IJourneyFareCalculationService>();
            _fareCalculationService = new FareCalculationService(_journeyFareCalculationService.Object);
        }

        [Theory]
        [InlineData(5, 4, 4)]
        [InlineData(4, 4, 4)]
        public void CalculateCost_ShouldReturnReminder_IfMonthlyCapIsGoingToBeExceeded(double maxSingleCost, double monthlyMax, double expected)
        {
            _journeyFareCalculationService.Setup(x => x.Calculate(It.IsAny<Journey>()))
                .Returns(new JourneyFare { MaxSingleCost = maxSingleCost, MonthlyMax = monthlyMax });

            var result = _fareCalculationService.CalculateCost(new Journey { Start = new Station(), End = new Station() }, new Models.ClamCard(100));
            Assert.Equal(expected, result);
        }

        [Theory]
        [InlineData(5, 4, 4)]
        [InlineData(4, 4, 4)]
        public void CalculateCost_ShouldReturnReminder_IfWeeklyCapIsGoingToBeExceeded(double maxSingleCost, double weeklyMax, double expected)
        {
            _journeyFareCalculationService.Setup(x => x.Calculate(It.IsAny<Journey>()))
                .Returns(new JourneyFare { MaxSingleCost = maxSingleCost, MonthlyMax = 100, WeeklyMax = weeklyMax });

            var result = _fareCalculationService.CalculateCost(new Journey { Start = new Station(), End = new Station() }, new Models.ClamCard(100));
            Assert.Equal(expected, result);
        }

        [Theory]
        [InlineData(5, 4, 4)]
        [InlineData(4, 4, 4)]
        public void CalculateCost_ShouldReturnReminder_IfDailyCapIsGoingToBeExceeded(double maxSingleCost, double dailyMax, double expected)
        {
            _journeyFareCalculationService.Setup(x => x.Calculate(It.IsAny<Journey>()))
                .Returns(new JourneyFare { MaxSingleCost = maxSingleCost, MonthlyMax = 100, WeeklyMax = 50, DailyMax = dailyMax });

            var result = _fareCalculationService.CalculateCost(new Journey { Start = new Station(), End = new Station() }, new Models.ClamCard(100));
            Assert.Equal(expected, result);
        }
    }
}