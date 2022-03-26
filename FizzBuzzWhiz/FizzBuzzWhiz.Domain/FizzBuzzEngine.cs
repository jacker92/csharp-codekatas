namespace FizzBuzzWhiz.Domain
{
    public class FizzBuzzEngine
    {
        public string Process(int input)
        {
            if (input % 3 == 0)
            {
                return "Fizz";
            }
            return input.ToString();
        }
    }
}