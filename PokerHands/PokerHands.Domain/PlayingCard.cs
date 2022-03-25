namespace PokerHands.Domain
{
    public class PlayingCard
    {
        public PlayingCard(Suit club, int value)
        {
            if (value > 14 || value < 1)
            {
                throw new ArgumentOutOfRangeException(nameof(value));
            }

            Suit = club;
            Value = value;
        }

        public Suit Suit { get; }
        public int Value { get; }

        public override string? ToString()
        {
            return $"{GetValue()}{Suit.ToString().Substring(0, 1).ToUpper()}";
        }

        private string GetValue()
        {
            if (Value == 1) return "A";
            return Value.ToString();
        }
    }
}