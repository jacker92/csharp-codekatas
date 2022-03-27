namespace GreetingKata.Domain
{
    public class Greeter
    {
        public string Greet(string name)
        {
            if (string.IsNullOrWhiteSpace(name))
            {
                throw new ArgumentException($"'{nameof(name)}' cannot be null or whitespace.", nameof(name));
            }

            return $"Hello, {name}";
        }
    }
}