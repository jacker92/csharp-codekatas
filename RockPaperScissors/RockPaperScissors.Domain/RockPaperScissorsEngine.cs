namespace RockPaperScissors.Domain
{
    public class RockPaperScissorsEngine
    {
        public Gesture CalculateWinner(Gesture player1, Gesture player2)
        {
            switch (player1)
            {
                case Gesture.Rock:
                    return HandleRock(player2);
                case Gesture.Paper:
                    return HandlePaper(player2);
                case Gesture.Scissors:
                    return HandleScissors(player2);
                default:
                    break;
            }
            return Gesture.Rock;
        }

        private static Gesture HandleScissors(Gesture player2)
        {
            if (player2 == Gesture.Rock)
            {
                return Gesture.Rock;
            }
            return Gesture.Scissors;
        }

        private static Gesture HandlePaper(Gesture player2)
        {
            if (player2 == Gesture.Scissors)
            {
                return Gesture.Scissors;
            }
            return Gesture.Paper;
        }

        private static Gesture HandleRock(Gesture player2)
        {
            if (player2 == Gesture.Paper)
            {
                return Gesture.Paper;
            }
            return Gesture.Rock;
        }
    }
}