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
        public void ThenMichaelWillBeChargedForHisFirstJourney(double p0)
        {
            Assert.Equal(_startingBalance - p0, _clamCard.Balance);
        }
    }
}
