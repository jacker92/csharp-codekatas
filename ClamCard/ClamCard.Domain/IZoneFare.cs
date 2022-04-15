namespace ClamCard.Domain
{
    public interface IZoneFare
    {
        Zone Zone { get; }
        double Single { get; }
        double Day { get; }
        double Week { get; }
        double Month { get; }
    }
}