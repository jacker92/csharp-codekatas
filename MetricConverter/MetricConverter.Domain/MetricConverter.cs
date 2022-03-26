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

            return kilometers * ConversionRates.KilometersToMiles;
        }
    }
}