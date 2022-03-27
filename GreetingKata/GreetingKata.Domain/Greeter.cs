using System.Text;

namespace GreetingKata.Domain
{
    public class Greeter
    {
        public string Greet(string[] names)
        {
            var normalNames = names.Where(x => !x.All(c => char.IsUpper(c)))
                .ToArray();

            var shoutedNames = names.Where(x => x.All(c => char.IsUpper(c)))
                .ToArray();

            var builder = new StringBuilder();

            builder.Append($"Hello, {string.Join(", ", normalNames, 0, normalNames.Length - 1)}{AddExtraComma(normalNames)} and {normalNames.Last()}");

            if (shoutedNames.Length > 0)
            {
                builder.Append($". AND HELLO {shoutedNames.First()}!");
            }

            return builder.ToString();
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