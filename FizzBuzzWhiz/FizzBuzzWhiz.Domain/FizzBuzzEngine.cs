using System.Text;

namespace FizzBuzzWhiz.Domain
{
    public class FizzBuzzEngine
    {
        public string Process(int input)
        {
            var builder = new StringBuilder();
            if (input % 3 == 0)
            {
                builder.Append("Fizz");
            }

            if (input % 5 == 0)
            {
                builder.Append("Buzz");
            }

            return string.IsNullOrEmpty(builder.ToString()) ? 
                input.ToString() : 
                builder.ToString();
        }
    }
}