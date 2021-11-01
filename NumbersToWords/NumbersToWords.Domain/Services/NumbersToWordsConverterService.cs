using NumbersToWords.Domain.Languages;
using System;
using System.Collections.Generic;
using System.Linq;

namespace NumbersToWords.Domain.Services
{
    public class NumbersToWordsConverterService
    {
        private readonly ITranslationService _translationService;
        private readonly ILanguageFeatureService _languageFeatureService;
        private readonly INumberParserService _numberParserService;

        public NumbersToWordsConverterService()
        {
            var languageService = new LanguageService();
            _translationService = new TranslationService(languageService);
            _languageFeatureService = new LanguageFeatureService(languageService);
            _numberParserService = new NumberParserService(new NumberProcessorService(), _languageFeatureService, _translationService);
        }

        public string Convert(int value, Language language = Language.English)
        {
            if (value < 0)
            {
                throw new ArgumentOutOfRangeException(nameof(value));
            }

            if (value == 0)
            {
                return _translationService.Translate(0, language);
            }

            return ExecuteConversion(value, language);
        }

        private string ExecuteConversion(int value, Language language)
        {
            var values = new List<string>();

            var sevenEightAndNineDigitNumbers = _numberParserService.ParseSevenEightAndNineDigitNumbers(value, language);
            var fourFiveAndSixDigitNumbers = _numberParserService.ParseFourFiveAndSixDigitNumbers(value, language);

            values.AddRange(sevenEightAndNineDigitNumbers);
            values.AddRange(fourFiveAndSixDigitNumbers);

            var threeDigits = _numberParserService.ParseThreeDigitNumbers(value, language);
            var twoDigits = _numberParserService.ParseTwoDigitNumbers(value, language);

            var concatted = AddThreeAndTwoDigits(language, threeDigits, twoDigits);
            values.AddRange(concatted);

            values = NormalizeValues(values);

            return _languageFeatureService.UsesSpacesBetweenNumberGroups(language) ?
                string.Join(' ', values) :
                string.Concat(values);
        }

        private IList<string> AddThreeAndTwoDigits(Language language, string threeDigits, string twoDigits)
        {
            if (!_languageFeatureService.UsesSpacesBetweenNumbers(language))
            {
                return new List<string> { string.Concat(threeDigits) + string.Concat(twoDigits) };
            }

            return new List<string> { threeDigits, twoDigits };
        }

        private static List<string> NormalizeValues(List<string> values)
        {
            return values.Where(x => !string.IsNullOrWhiteSpace(x))
                           .Select(x => x.Replace("ttt", "tt"))
                           .ToList();
        }
    }
}
