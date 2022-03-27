namespace GreetingKata.Domain
{
    public class Greeter
    {
        public string Greet(string name)
        {
            name ??= "my friend";

            return $"Hello, {name}";
        }
    }
}