using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FizzBuzzWhiz.Domain
{
    public static class IntExtensions
    {
        public static bool IsPrime(this int number)
        {
            if (number <= 1)
            {
                return false;
            }

            for (int i = number - 1; i > 1; i--)
            {
                if (number % i == 0)
                {
                    return false;
                }
            }

            return true;
        }
    }
}
