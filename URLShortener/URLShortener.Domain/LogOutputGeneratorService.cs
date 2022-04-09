using System.Text;

namespace URLShortener.Domain
{
    public class LogOutputGeneratorService
    {
        public static string Generate(UrlStatistics statistics)
        {
            var builder = new StringBuilder();
            builder.Append($"#Log info for url: {statistics.LongUrl}({statistics.ShortUrl})#\n");
            builder.Append($"Number of accesses: {statistics.TimesAccessed.Count}\n");

            for (int i = 1; i <= statistics.TimesAccessed.Count; i++)
            {
                builder.Append($"Access #{i}: {statistics.TimesAccessed.First().Timestamp:G}\n");
            }

            return builder.ToString();
        }
    }
}