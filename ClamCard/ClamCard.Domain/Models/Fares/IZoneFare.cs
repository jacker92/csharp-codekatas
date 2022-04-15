namespace ClamCard.Domain.Models.Fares
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