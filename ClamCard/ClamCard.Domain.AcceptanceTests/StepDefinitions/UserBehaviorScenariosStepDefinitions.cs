using System;
using TechTalk.SpecFlow;

namespace ClamCard.Domain.AcceptanceTests.StepDefinitions
{
    [Binding]
    public class UserBehaviorScenariosStepDefinitions
    {
        private readonly User _user;
        private readonly ClamCard _clamCard;
        private readonly double _startingBalance;
        private readonly TravelService _travelService;

        public UserBehaviorScenariosStepDefinitions()
        {
            _user = new User("Michael");
            _startingBalance = 100;
            _clamCard = new ClamCard(_startingBalance);
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
            _travelService.Travel(_user, new Journey { Start = new Station { Name = "Asterisk", Zone = Zone.A }, End = new Station { Name = "Aldgate", Zone = Zone.A } });
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
            _travelService.Travel(_user, new Journey { Start = new Station { Name = "Asterisk", Zone = Zone.A }, End = new Station { Name = "Barbican", Zone = Zone.B } });
        }

        [Given(@"Michael travels from Asterisk to Balham")]
        public void GivenMichaelTravelsFromAsteriskToBalham()
        {
            _travelService.Travel(_user, new Journey { Start = new Station { Name = "Asterisk", Zone = Zone.A }, End = new Station { Name = "Balham", Zone = Zone.B } });
        }

        [Then(@"a further \$(.*) for his second journey")]
        public void ThenAFurtherForHisSecondJourney(double chargedAmount)
        {
            _clamCard.TravellingHistory[1].Cost.Should().Be(chargedAmount);
            _clamCard.Balance.Should().Be(_startingBalance - _clamCard.TravellingHistory.Sum(x => x.Cost));
        }
    }
}
