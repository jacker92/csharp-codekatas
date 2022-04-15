namespace ClamCard.Domain.Models.Fares
{
    public class ZoneAFare : IZoneFare
    {
        public Zone Zone => Zone.A;

        public double Single => 2.5;

        public double Day => 7.0;

        public double Week => 40;

        public double Month => 145;

        public double SingleReturn => 2;
    }
}