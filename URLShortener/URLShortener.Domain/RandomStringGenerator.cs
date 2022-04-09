namespace URLShortener.Domain
{
    public static class RandomStringGenerator
    {
        public static string Generate(int length)
        {
            var random = new Random();
            const string chars = "abcedfghijklmnopqrstuvwxyz0123456789";
            return new string(Enumerable.Repeat(chars, length)
                .Select(s => s[random.Next(s.Length)]).ToArray());
        }
    }
}