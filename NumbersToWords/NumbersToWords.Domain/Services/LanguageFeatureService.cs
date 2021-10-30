using System.Collections.Generic;
using System.Linq;

namespace NumbersToWords.Domain
{
    public class LanguageFeatureService : ILanguageFeatureService
    {
        private readonly List<ILanguageFeatures> _languageFeatures = new List<ILanguageFeatures>
        {
            new EnglishLanguageFeatures(),
            new FinnishLanguageFeatures()
        };

        public bool UsesDashes(Language language)
        {
            return _languageFeatures.Single(x => x.Language == language)
                                    .UsesDashes;
        }
    }
}
