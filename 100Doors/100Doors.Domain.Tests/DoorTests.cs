using _100Doors.Domain.Models;
using Xunit;

namespace _100Doors.Console.Tests
{
    public class DoorTests
    {
        private readonly Door _door;

        public DoorTests()
        {
            _door = new Door();
        }

        [Fact]
        public void ShouldHaveClosedState_AfterInitialization()
        {
            Assert.Equal(DoorState.Closed, _door.State);
        }

        [Fact]
        public void ShouldHaveOpenState_AfterToggle()
        {
            _door.Toggle();
            Assert.Equal(DoorState.Open, _door.State);
        }

        [Fact]
        public void ShouldHaveClosedState_AfterTwoToggles()
        {
            _door.Toggle();
            _door.Toggle();
            Assert.Equal(DoorState.Closed, _door.State);
        }

        [Fact]
        public void ShouldPrintNumberSign_WhenDoorStateIsClosed()
        {
            Assert.Equal("#", _door.ToString());
        }

        [Fact]
        public void ShouldPrintAtSymbol_WhenDoorStateIsOpen()
        {
            _door.Toggle();
            Assert.Equal("@", _door.ToString());
        }
    }
}