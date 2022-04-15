namespace ClamCard.Domain
{
    public class User
    {
        public User(string name)
        {
            if (string.IsNullOrWhiteSpace(name))
            {
                throw new ArgumentException($"'{nameof(name)}' cannot be null or whitespace.", nameof(name));
            }

            Name = name;
        }

        public string Name { get; }
        public ClamCard ClamCard { get; private set; }

        public void AddClamCard(ClamCard clamCard)
        {
            if (clamCard is null)
            {
                throw new ArgumentNullException(nameof(clamCard));
            }

            ClamCard = clamCard;
        }
    }
}