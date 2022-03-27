namespace GreetingKata.Domain
{
    public class Greeter
    {
        public string Greet(string name)
        {
            name ??= "my friend";

            if (name.All(x => char.IsUpper(x)))
            {
                return $"HELLO {name}!";
            }

            return $"Hello, {name}";
        }
    }
}