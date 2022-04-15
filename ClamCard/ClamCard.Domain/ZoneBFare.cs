namespace ClamCard.Domain
{
    public class ZoneBFare : IZoneFare
    {
        public Zone Zone => Zone.B;

        public double Single => 3;

        public double Day => 8;

        public double Week => 47;

        public double Month => 165;
    }
}