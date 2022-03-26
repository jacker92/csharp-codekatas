namespace MetricConverter.Domain
{
    public class MetricConverter
    {
        public double ConvertKilometersToMiles(double kilometers)
        {
            if (kilometers < 0)
            {
                throw new ArgumentOutOfRangeException(nameof(kilometers));
            }

            return kilometers * Conversions.KilometersToMiles;
        }

        public double ConvertCelsiusToFahrenheit(double celsius)
        {
            if (celsius < Constants.AbsoluteZero)
            {
                throw new ArgumentOutOfRangeException(nameof(celsius), "Argument cannot be less than absolute zero.");
            }
            return celsius * 1.8 + 32;
        }
    }
}