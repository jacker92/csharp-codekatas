using ClamCard.Domain.Models;
using ClamCard.Domain.Models.Fares;

namespace ClamCard.Domain.Factories
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