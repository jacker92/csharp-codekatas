using ClamCard.Domain.Exceptions;

namespace ClamCard.Domain.Models
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

            if (ClamCard != null)
            {
                throw new ClamCardAlreadyExistsException();
            }

            ClamCard = clamCard;
        }
    }
}