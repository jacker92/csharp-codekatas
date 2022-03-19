namespace MemoryCache.Domain
{
    public class MemoryCacheOptions
    {
        public TimeSpan ItemTimeToLive { get; set; } = TimeSpan.FromSeconds(60);
        public int Capacity { get; set; } = 100000;
    }

    public class MemoryCache
    {
        private readonly IDictionary<string, MemoryCacheItem> _items;
        public int Capacity { get; }
        public int CurrentCapacity => _items.Count();
        public TimeSpan TimeToLive { get; }

        public MemoryCache(MemoryCacheOptions memoryCacheOptions)
        {
            if (memoryCacheOptions.Capacity <= 0)
            {
                throw new ArgumentOutOfRangeException(nameof(memoryCacheOptions.Capacity));
            }

            _items = new Dictionary<string, MemoryCacheItem>();
            Capacity = memoryCacheOptions.Capacity;
            TimeToLive = memoryCacheOptions.ItemTimeToLive;
        }

        public MemoryCache() : this(new MemoryCacheOptions())
        {

        }

        public MemoryCacheItem GetItem(string key)
        {
            if (string.IsNullOrWhiteSpace(key))
            {
                throw new ArgumentException($"'{nameof(key)}' cannot be null or whitespace.", nameof(key));
            }

            if (!_items.ContainsKey(key))
            {
                throw new KeyNotFoundException($"Key: {key} cannot be found from MemoryCache.");
            }

            return _items[key];
        }

        public void SetItem(string key, object value)
        {
            if (string.IsNullOrWhiteSpace(key))
            {
                throw new ArgumentException($"'{nameof(key)}' cannot be null or whitespace.", nameof(key));
            }

            if (value is null)
            {
                throw new ArgumentNullException(nameof(value));
            }

            var memoryCacheItem = new MemoryCacheItem(value, TimeToLive);

            if (CurrentCapacity == Capacity)
            {
                var keyToRemove = _items.MinBy(x => x.Value.TimeToLive).Key;
                _items.Remove(keyToRemove);
            }

            _items.Add(key, memoryCacheItem);
        }
    }
}