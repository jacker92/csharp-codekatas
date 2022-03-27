namespace GreetingKata.Domain
{
    public class Greeter
    {
        public string Greet(string[] names)
        {
            return "Hello, " + string.Join(" and ", names);
        }

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