using System;
using TechTalk.SpecFlow;

namespace ClamCard.Domain.AcceptanceTests.StepDefinitions
{
    [Binding]
    public class UserBehaviorScenariosStepDefinitions
    {
        private readonly User _user;

        public UserBehaviorScenariosStepDefinitions()
        {
            _user = new User("Michael");
        }

        [Given(@"Michael has an Clam Card")]
        public void GivenMichaelHasAnClamCard()
        {
            throw new PendingStepException();
        }

        [Given(@"Michael travels from Asterisk to Aldgate")]
        public void GivenMichaelTravelsFromAsteriskToAldgate()
        {
            throw new PendingStepException();
        }

        [Then(@"Michael will be charged \$(.*) for his first journey")]
        public void ThenMichaelWillBeChargedForHisFirstJourney(Decimal p0)
        {
            throw new PendingStepException();
        }
    }
}
