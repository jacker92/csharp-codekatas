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

        public UserBehaviorScenariosStepDefinitions()
        {
            _user = new User("Michael");
            _startingBalance = 100;
            _clamCard = new ClamCard(_startingBalance);
        }

        [Given(@"Michael has an Clam Card")]
        public void GivenMichaelHasAnClamCard()
        {
            _user.AddClamCard(_clamCard);
        }

        [Given(@"Michael travels from Asterisk to Aldgate")]
        public void GivenMichaelTravelsFromAsteriskToAldgate()
        {
            throw new PendingStepException();
        }

        [Then(@"Michael will be charged \$(.*) for his first journey")]
        public void ThenMichaelWillBeChargedForHisFirstJourney(double p0)
        {
            Assert.Equal(_startingBalance - p0, _clamCard.Balance);
        }
    }
}
