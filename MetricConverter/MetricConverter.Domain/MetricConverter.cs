namespace MetricConverter.Domain
{
    public class MetricConverter
    {
        public void ConvertKilometersToMiles(int kilometers)
        {
            if (kilometers < 0)
            {
                throw new ArgumentOutOfRangeException(nameof(kilometers));
            }

            throw new NotImplementedException();
        }
    }
}