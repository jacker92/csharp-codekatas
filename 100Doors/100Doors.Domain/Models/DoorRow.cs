using System;
using System.Linq;

namespace _100Doors.Domain.Models
{
    public class DoorRow
    {
        private readonly Door[] _doors;
        private int _passCount = 1;

        public DoorRow(int doorCount)
        {
            if (doorCount <= 0)
            {
                throw new ArgumentOutOfRangeException(nameof(doorCount), "Value cannot be zero or negative.");
            }

            _doors = Enumerable.Range(0, doorCount)
                .Select(x => new Door())
                .ToArray();
        }

        public int DoorCount()
        {
            return _doors.Length;
        }

        public override string ToString()
        {
            return string.Join("", _doors.Select(x => x.ToString()));
        }

        public void Pass()
        {
            for (int i = 0; i < _doors.Length; i++)
            {
                if ((i + 1) % _passCount == 0)
                {
                    _doors[i].Toggle();
                }
            }

            _passCount++;
        }

        public void Pass(int times)
        {
            if (times <= 0)
            {
                throw new ArgumentOutOfRangeException(nameof(times), "Value cannot be zero or negative.");
            }

            for (int i = 0; i < times; i++)
            {
                Pass();
            }
        }
    }
}
