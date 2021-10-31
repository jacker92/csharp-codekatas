using NumbersToWords.Domain.Languages;
using NumbersToWords.Domain.Utils;
using System;
using System.Collections.Generic;
using System.Linq;

namespace NumbersToWords.Domain.Services
{
    public class NumbersToWordsConverterService
    {
        private readonly NumberProcessorService _numberProcessor;
        private readonly ITranslationService _translationService;
        private readonly ILanguageFeatureService _languageFeatureService;

        public NumbersToWordsConverterService()
        {
            var languageService = new LanguageService();
            _translationService = new TranslationService(languageService);
            _numberProcessor = new NumberProcessorService();
            _languageFeatureService = new LanguageFeatureService(languageService);
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

            var values = new List<string>();
            values.AddRange(ParseSevenEightAndNineDigitNumbers(value, language));
            values.AddRange(ParseFourFiveAndSixDigitNumbers(value, language));

            var threeDigits = ParseThreeDigitNumbers(value, language);
            var twoDigits = ParseTwoDigitNumbers(value, language);

            if (!_languageFeatureService.UsesSpacesBetweenNumbers(language))
            {
                values.Add(string.Concat(threeDigits) + string.Concat(twoDigits));
            }
            else
            {
                values.AddRange(threeDigits);
                values.AddRange(twoDigits);
            }

            values = NormalizeValues(values);

            return _languageFeatureService.UsesSpacesBetweenNumberGroups(language) ?
                string.Join(' ', values) :
                string.Concat(values);
        }

        private static List<string> NormalizeValues(List<string> values)
        {
            return values.Where(x => !string.IsNullOrWhiteSpace(x))
                           .Select(x => x.Replace("ttt", "tt"))
                           .ToList();
        }

        private IList<string> ParseSevenEightAndNineDigitNumbers(int value, Language language)
        {
            return ParseNumbers(value, language, Constants.Million, _numberProcessor.GetAmountOfMillions);
        }

        private IList<string> ParseFourFiveAndSixDigitNumbers(int value, Language language)
        {
            return ParseNumbers(value, language, Constants.Thousand, _numberProcessor.GetAmountOfThousands);
        }

        private IList<string> ParseNumbers(int value, Language language, int minNumberToParse, Func<int, int> extractionFunc)
        {
            var list = new List<string>();

            if (value >= minNumberToParse)
            {
                var amountOfNumbers = extractionFunc(value);

                if (amountOfNumbers == 0)
                {
                    return list;
                }

                if (amountOfNumbers > 1 || _languageFeatureService.SingleUnitIsSpecifiedAsADigit(language))
                {
                    var threeDigitNumbers = ParseThreeDigitNumbers(amountOfNumbers, language);
                    var twoDigitNumbers = ParseTwoDigitNumbers(amountOfNumbers, language);

                    if (amountOfNumbers >= 100)
                    {
                        list.AddRange(threeDigitNumbers);
                    }

                    list.AddRange(twoDigitNumbers);
                }

                var number = _translationService.Translate(minNumberToParse, language);

                if (amountOfNumbers > 1 && _languageFeatureService.UsesPluralizedForms(language))
                {
                    number += _languageFeatureService.GetPluralizedForm(language, number);
                }

                list.Add(number);
            }

            if (!_languageFeatureService.UsesSpacesBetweenNumbers(language))
            {
                return new List<string> { string.Concat(list) };
            }

            return list;
        }

        private IList<string> ParseThreeDigitNumbers(int value, Language language)
        {
            var threeDigits = _numberProcessor.GetThreeDigitNumber(value);
            var list = new List<string>();

            if (threeDigits >= 100)
            {
                var oneDigit = _numberProcessor.GetFirstDigit(threeDigits);

                string result = string.Empty;
                if (oneDigit != 1 || _languageFeatureService.SingleUnitIsSpecifiedAsADigit(language))
                {
                    result += _translationService.Translate(oneDigit, language);
                }

                if (_languageFeatureService.UsesSpacesBetweenNumbers(language))
                {
                    result += Constants.Space;
                }

                var hundred = _translationService.Translate(100, language);
                result += hundred;

                if (oneDigit != 1 && _languageFeatureService.UsesPluralizedForms(language))
                {
                    result += _languageFeatureService.GetPluralizedForm(language, hundred);
                }

                list.Add(result);
            }

            return list;
        }

        private IList<string> ParseTwoDigitNumbers(int value, Language language)
        {
            var twoDigits = _numberProcessor.GetTwoDigitNumber(value);
            var values = ParseTwoDigitsOverTwenty(twoDigits, language);

            if (twoDigits < 21 && twoDigits != 0)
            {
                values.Add(_translationService.Translate(twoDigits, language));
            }

            return values;
        }

        private List<string> ParseTwoDigitsOverTwenty(int twoDigits, Language language)
        {
            var values = new List<string>();

            if (twoDigits > 20)
            {
                var lastDigit = _numberProcessor.GetLastDigit(twoDigits);
                var evenTwoDigitNumber = _numberProcessor.GetEvenTwoDigitNumber(twoDigits);

                var translation = _translationService.Translate(evenTwoDigitNumber, language);
                var secondDigit = AddSecondDigit(language, lastDigit);
                values.Add(translation + secondDigit);
            }

            return values;
        }

        private string AddSecondDigit(Language language, int lastDigit)
        {
            var digits = string.Empty;

            if (lastDigit == 0)
            {
                return digits;
            }

            if (_languageFeatureService.UsesDashes(language))
            {
                digits += Constants.Dash;
            }

            return digits + _translationService.Translate(lastDigit, language);
        }
    }
}
