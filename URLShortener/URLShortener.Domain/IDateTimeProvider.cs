namespace URLShortener.Domain
{
    public interface IDateTimeProvider
    {
        DateTime DateTimeNow { get; }
    }
}