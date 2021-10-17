using System.Collections.Generic;

namespace _100Doors.Domain.Models
{
    public class Door
    {
        private readonly Dictionary<DoorState, string> _stateCharactersMap;

        public Door()
        {
            _stateCharactersMap = new Dictionary<DoorState, string>
            {
                { DoorState.Open, "@" },
                { DoorState.Closed, "#" }
            };
        }

        public DoorState State { get; private set; } = DoorState.Closed;

        public void Toggle()
        {
            State = State == DoorState.Open ? DoorState.Closed : DoorState.Open;
        }

        public override string ToString()
        {
            return _stateCharactersMap[State];
        }
    }
}
