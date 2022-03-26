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

        public double ConvertKilogramToPound(double kilograms)
        {
            if (kilograms < 0)
            {
                throw new ArgumentOutOfRangeException(nameof(kilograms));
            }

            return kilograms / Conversions.KilogramsToPounds;
        }

        public double ConvertLitersToGallons(double liters, GallonTargetUnit targetUnit)
        {
            if (liters < 0)
            {
                throw new ArgumentOutOfRangeException(nameof(liters));
            }

            if (targetUnit == GallonTargetUnit.UK)
            {
                return liters / Conversions.LitersToUKGallons;
            }

            return liters / Conversions.LitersToUSGallons;
        }
    }
}