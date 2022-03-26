namespace RockPaperScissors.Domain
{
    public class RockPaperScissorsEngine
    {
        public Gesture CalculateWinner(Gesture player1, Gesture player2)
        {
            switch (player1)
            {
                case Gesture.Rock:
                    if (player2 == Gesture.Paper)
                    {
                        return Gesture.Paper;
                    }
                    return Gesture.Rock;
                case Gesture.Paper:
                    if(player2 == Gesture.Scissors)
                    {
                        return Gesture.Scissors;
                    }
                    break;
                case Gesture.Scissors:
                    if (player2 == Gesture.Rock)
                    {
                        return Gesture.Rock;
                    }
                    return Gesture.Scissors;
                default:
                    break;
            }
            return Gesture.Rock;
        }
    }
}