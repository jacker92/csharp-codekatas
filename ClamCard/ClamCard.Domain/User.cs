namespace ClamCard.Domain
{
    public class User
    {
        private readonly string _name;

        public User(string name)
        {
            if (string.IsNullOrWhiteSpace(name))
            {
                throw new ArgumentException($"'{nameof(name)}' cannot be null or whitespace.", nameof(name));
            }

            _name = name;
        }
    }
}