using System;
using System.Linq;

namespace MarsRover.Domain.Utils
{
    public static class EnumHelpers
    {
        public static T GetLast<T>(this T t) where T : Enum
        {
            return Enum.GetValues(typeof(T)).Cast<T>().Last();
        }

        public static T GetFirst<T>(this T t) where T : Enum
        {
            return Enum.GetValues(typeof(T)).Cast<T>().First();
        }
    }
}

