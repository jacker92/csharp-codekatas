namespace MemoryCache.Domain
{
    public class MemoryCacheOptions
    {
        private TimeSpan itemTimeToLive = TimeSpan.FromSeconds(60);
        private int capacity = 100000;

        public TimeSpan ItemTimeToLive
        {
            get => itemTimeToLive;
            set
            {
                if (value <= TimeSpan.Zero)
                {
                    throw new ArgumentOutOfRangeException(nameof(value));
                }

                itemTimeToLive = value;
            }
        }
        public int Capacity
        {
            get => capacity;
            set
            {
                if (value <= 0)
                {
                    throw new ArgumentOutOfRangeException(nameof(value));
                }

                capacity = value;
            }
        }
    }
}