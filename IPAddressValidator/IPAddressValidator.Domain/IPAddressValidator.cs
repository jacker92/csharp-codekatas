using System;
using System.Linq;

namespace IPAddressValidator.Domain
{
    public class IPAddressValidator
    {
        public bool ValidateIpv4Address(string ipAddress)
        {
            if (string.IsNullOrWhiteSpace(ipAddress))
            {
                throw new ArgumentException($"'{nameof(ipAddress)}' cannot be null or whitespace.", nameof(ipAddress));
            }

            var splitted = ipAddress.Split('.');

            if (!ValidateArray(splitted))
            {
                return false;
            }

            var numbers = ConvertToNumbers(splitted);

            return NumbersAreInRange(numbers);
        }

        private bool ValidateArray(string[] splitted)
        {
            return splitted.Length == 4 &&
                splitted.All(x => int.TryParse(x, out int _));
        }

        private int[] ConvertToNumbers(string[] splitted)
        {
            return splitted.Select(x => int.Parse(x)).ToArray();
        }

        private bool NumbersAreInRange(int[] numbers)
        {
            for (int i = 0; i < numbers.Length; i++)
            {
                var current = numbers[i];

                if (IsValidMiddleNumber(i, current) || IsValidStartOrEndNumber(i, current))
                {
                    return false;
                }
            }

            return true;
        }

        private static bool IsValidStartOrEndNumber(int iteration, int number)
        {
            return (iteration == 0 || iteration == 3) && number < 1 || number > 254;
        }

        private static bool IsValidMiddleNumber(int iteration, int number)
        {
            return (iteration == 1 || iteration == 2) && (number < 0 || number > 255);
        }
    }
}
