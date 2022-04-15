namespace ClamCard.Domain
{
    public class FareFactory
    {
        public IZoneFare GetFareFor(Zone zone)
        {
            return zone switch
            {
                Zone.A => new ZoneAFare(),
                Zone.B => new ZoneBFare(),
                _ => throw new Exception(),
            };
        }
    }
}