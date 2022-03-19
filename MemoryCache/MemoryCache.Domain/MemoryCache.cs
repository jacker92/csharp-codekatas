namespace MemoryCache.Domain
{
    public class MemoryCache
    {
        private readonly IDictionary<string, MemoryCacheItem> _items;
        private readonly MemoryCacheOptions _memoryCacheOptions;

        public int Capacity => _memoryCacheOptions.Capacity;
        public int CurrentCapacity => _items.Count();
        public TimeSpan TimeToLive => _memoryCacheOptions.ItemTimeToLive;

        public MemoryCache(MemoryCacheOptions memoryCacheOptions)
        {
            _memoryCacheOptions = memoryCacheOptions ?? throw new ArgumentNullException(nameof(memoryCacheOptions));

            _items = new Dictionary<string, MemoryCacheItem>();
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