namespace GreetingKata.Domain
{
    public class Greeter
    {
        public string Greet(string[] names)
        {
            return $"Hello, {string.Join(", ", names, 0, names.Length - 1)}{AddExtraComma(names)} and {names.Last()}";
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

        private string AddExtraComma(string[] names)
        {
            return names.Length > 2 ? "," : string.Empty;
        }
    }
}