using ClamCard.Domain.Models;
using ClamCard.Domain.Services;

namespace ClamCard.Domain.AcceptanceTests.StepDefinitions
{
    [Binding]
    public class UserBehaviorScenariosStepDefinitions
    {
        private readonly User _user;
        private readonly Models.ClamCard _clamCard;
        private readonly double _startingBalance;
        private readonly TravelService _travelService;
        private DateTime _startingDate;

        public UserBehaviorScenariosStepDefinitions()
        {
            _startingDate = DateTime.Parse("1.1.2000");
            _user = new User("Michael");
            _startingBalance = 10000;
            _clamCard = new Models.ClamCard(_startingBalance);
            _travelService = new TravelService();
        }

        [Given(@"Michael has an Clam Card")]
        public void GivenMichaelHasAnClamCard()
        {
            _user.AddClamCard(_clamCard);
        }

        [Given(@"Michael travels from Asterisk to Aldgate")]
        public void GivenMichaelTravelsFromAsteriskToAldgate()
        {
            _travelService.Travel(_user, new Journey { Start = new Station { Name = "Asterisk", Zone = Zone.A, Date = _startingDate }, End = new Station { Name = "Aldgate", Zone = Zone.A, Date = _startingDate } });
        }

        [Then(@"Michael will be charged \$(.*) for his first journey")]
        public void ThenMichaelWillBeChargedForHisFirstJourney(double chargedAmount)
        {
            _clamCard.TravellingHistory.First().Cost.Should().Be(chargedAmount);
            _clamCard.Balance.Should().Be(_startingBalance - _clamCard.TravellingHistory.Sum(x => x.Cost));
        }

        [Given(@"Michael travels from Asterisk to Barbican")]
        public void GivenMichaelTravelsFromAsteriskToBarbican()
        {
            _travelService.Travel(_user, new Journey { Start = new Station { Name = "Asterisk", Zone = Zone.A, Date = _startingDate }, End = new Station { Name = "Barbican", Zone = Zone.B, Date = _startingDate } });
        }

        [Given(@"Michael travels from Asterisk to Balham")]
        public void GivenMichaelTravelsFromAsteriskToBalham()
        {
            _travelService.Travel(_user, new Journey { Start = new Station { Name = "Asterisk", Zone = Zone.A, Date = _startingDate }, End = new Station { Name = "Balham", Zone = Zone.B, Date = _startingDate } });
        }

        [Then(@"a further \$(.*) for his second journey")]
        public void ThenAFurtherForHisSecondJourney(double chargedAmount)
        {
            _clamCard.TravellingHistory[1].Cost.Should().Be(chargedAmount);
            _clamCard.Balance.Should().Be(_startingBalance - _clamCard.TravellingHistory.Sum(x => x.Cost));
        }

        [Given(@"Michael travels from Barbican to Balham")]
        public void GivenMichaelTravelsFromBarbicanToBalham()
        {
            _travelService.Travel(_user, new Journey { Start = new Station { Name = "Barbican", Zone = Zone.B , Date = _startingDate }, End = new Station { Name = "Balham", Zone = Zone.B, Date = _startingDate } });
        }

        [Given(@"Michael travels from Balham to Bison")]
        public void GivenMichaelTravelsFromBalhamToBison()
        {
            _travelService.Travel(_user, new Journey { Start = new Station { Name = "Balham", Zone = Zone.B, Date = _startingDate }, End = new Station { Name = "Bison", Zone = Zone.B, Date = _startingDate } });
        }

        [Given(@"Michael travels from Bison to Asterisk")]
        public void GivenMichaelTravelsFromBisonToAsterisk()
        {
            _travelService.Travel(_user, new Journey { Start = new Station { Name = "Bison", Zone = Zone.B, Date = _startingDate }, End = new Station { Name = "Asterix", Zone = Zone.A, Date = _startingDate } });
        }

        [Then(@"a further \$(.*) for his third journey")]
        public void ThenAFurtherForHisThirdJourney(double chargedAmount)
        {
            _clamCard.TravellingHistory[2].Cost.Should().Be(chargedAmount);
            _clamCard.Balance.Should().Be(_startingBalance - _clamCard.TravellingHistory.Sum(x => x.Cost));
        }

        [Then(@"a further \$(.*) for his fourth journey")]
        public void ThenAFurtherForHisFourthJourney(double chargedAmount)
        {
            _clamCard.TravellingHistory[3].Cost.Should().Be(chargedAmount);
            _clamCard.Balance.Should().Be(_startingBalance - _clamCard.TravellingHistory.Sum(x => x.Cost));
        }

        [Given(@"Michael travels from Aldgate to Angel")]
        public void GivenMichaelTravelsFromAldgateToAngel()
        {
            _travelService.Travel(_user, new Journey { Start = new Station { Name = "Aldgate", Zone = Zone.A, Date = _startingDate }, End = new Station { Name = "Angel", Zone = Zone.A, Date = _startingDate } });
        }

        [Given(@"Michael travels from Angel to Antelope")]
        public void GivenMichaelTravelsFromAngelToAntelope()
        {
            _travelService.Travel(_user, new Journey { Start = new Station { Name = "Angel", Zone = Zone.A, Date = _startingDate }, End = new Station { Name = "Antilope", Zone = Zone.A, Date = _startingDate } });
        }

        [Given(@"Michael travels from Antelope to Asterisk")]
        public void GivenMichaelTravelsFromAntelopeToAsterisk()
        {
            _travelService.Travel(_user, new Journey { Start = new Station { Name = "Antilope", Zone = Zone.A, Date = _startingDate }, End = new Station { Name = "Asterisk", Zone = Zone.A, Date = _startingDate } });
        }

        [Given(@"Michael sleeps for (.*) day")]
        public void GivenMichaelSleepsForDay(int daysToSleep)
        {
            _startingDate = _startingDate.AddDays(daysToSleep);
        }

        [Given(@"Michael travels reaching daily cap on zone A")]
        public void GivenMichaelTravelsReachingDailyCapOnZoneA()
        {
            GivenMichaelTravelsFromAngelToAntelope();
            GivenMichaelTravelsFromAldgateToAngel();
            GivenMichaelTravelsFromAntelopeToAsterisk();
        }

        [Given(@"Michael travels reaching daily cap on zone B")]
        public void GivenMichaelTravelsReachingDailyCapOnZoneB()
        {
            GivenMichaelTravelsFromAsteriskToBalham();
            GivenMichaelTravelsFromAsteriskToBarbican();
            GivenMichaelTravelsFromAsteriskToBalham();
        }

        [Then(@"Michael will be charged \$(.*) in total")]
        public void ThenMichaelWillBeChargedInTotal(double chargedAmount)
        {
            _clamCard.Balance.Should().Be(_startingBalance - _clamCard.TravellingHistory.Sum(x => x.Cost));
            _clamCard.Balance.Should().Be(_startingBalance - chargedAmount);
        }

        [Given(@"Michael travels for a week reaching weekly cap on zone A")]
        public void GivenMichaelTravelsForAWeekReachingWeeklyCapOnZoneA()
        {
            for (int i = 0; i < 7; i++)
            {
                GivenMichaelTravelsReachingDailyCapOnZoneA();
                GivenMichaelSleepsForDay(1);
            }
        }

        [Given(@"Michael travels for a week reaching weekly cap on zone B")]
        public void GivenMichaelTravelsForAWeekReachingWeeklyCapOnZoneB()
        {
            for (int i = 0; i < 7; i++)
            {
                GivenMichaelTravelsReachingDailyCapOnZoneB();
                GivenMichaelSleepsForDay(1);
            }
        }

        [Given(@"Michael travels from Barbican to Asterisk")]
        public void GivenMichaelTravelsFromBarbicanToAsterisk()
        {
            _travelService.Travel(_user, new Journey { Start = new Station { Name = "Barbican", Zone = Zone.B, Date = _startingDate }, End = new Station { Name = "Asterisk", Zone = Zone.A, Date = _startingDate } });
        }

        [Given(@"Michael travels from Balham to Asterisk")]
        public void GivenMichaelTravelsFromBalhamToAsterisk()
        {
            _travelService.Travel(_user, new Journey { Start = new Station { Name = "Balham", Zone = Zone.B, Date = _startingDate }, End = new Station { Name = "Asterisk", Zone = Zone.A, Date = _startingDate } });
        }
    }
}
