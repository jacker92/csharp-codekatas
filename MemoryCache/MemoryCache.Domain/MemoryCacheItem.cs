namespace MemoryCache.Domain
{
    public class MemoryCacheItem
    {
        public MemoryCacheItem(object item, TimeSpan timeToLive)
        {
            Item = item;
            TimeToLive = DateTime.UtcNow.Add(timeToLive);
        }
        public object Item { get; }
        public DateTime TimeToLive { get; }
    }
}