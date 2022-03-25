namespace PokerHands.Domain
{
    public class PlayingCard
    {
        public PlayingCard(Suit club, int value)
        {
            if (value > 13 || value < 1)
            {
                throw new ArgumentOutOfRangeException(nameof(value));
            }

            Suit = club;
            Value = value;
        }

        public Suit Suit { get; }
        public int Value { get; }

        public override bool Equals(object? obj)
        {
            return obj is PlayingCard card &&
                   Value == card.Value;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Value);
        }

        public override string? ToString()
        {
            return $"{GetValue()}{Suit.ToString().Substring(0, 1).ToUpper()}";
        }

        private string GetValue()
        {
            if (Value == 1) return "A";
            if (Value == 10) return "T";
            if (Value == 11) return "J";
            if (Value == 12) return "Q";
            if (Value == 13) return "K";
            return Value.ToString();
        }
    }
}