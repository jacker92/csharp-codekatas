namespace MemoryCache.Domain
{
    public class MemoryCache
    {
        public IDictionary<string, string> Items { get; } = new Dictionary<string, string>();

        public void GetItem(string key)
        {
            if (string.IsNullOrWhiteSpace(key))
            {
                throw new ArgumentException($"'{nameof(key)}' cannot be null or whitespace.", nameof(key));
            }

            if (!Items.ContainsKey(key))
            {
                throw new KeyNotFoundException($"Key: {key} cannot be found from MemoryCache.");
            }
        }

        public void SetItem(string key)
        {
            if (string.IsNullOrWhiteSpace(key))
            {
                throw new ArgumentException($"'{nameof(key)}' cannot be null or whitespace.", nameof(key));
            }
        }
    }
}