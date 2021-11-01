using NumbersToWords.Domain.Languages;
using NumbersToWords.Domain.Utils;
using System;
using System.Collections.Generic;
using System.Linq;

namespace NumbersToWords.Domain.Services
{
    public class NumberParserService : INumberParserService
    {
        private readonly NumberProcessorService _numberProcessorService;
        private readonly ILanguageFeatureService _languageFeatureService;
        private readonly ITranslationService _translationService;

        public NumberParserService(NumberProcessorService numberProcessorService, ILanguageFeatureService languageFeatureService, ITranslationService translationService)
        {
            _numberProcessorService = numberProcessorService ?? throw new ArgumentNullException(nameof(numberProcessorService));
            _languageFeatureService = languageFeatureService ?? throw new ArgumentNullException(nameof(languageFeatureService));
            _translationService = translationService ?? throw new ArgumentNullException(nameof(translationService));
        }

        public IList<string> ParseSevenEightAndNineDigitNumbers(int value, Language language)
        {
            return ParseNumbers(value, language, Constants.Million, _numberProcessorService.GetAmountOfMillions);
        }

        public IList<string> ParseFourFiveAndSixDigitNumbers(int value, Language language)
        {
            return ParseNumbers(value, language, Constants.Thousand, _numberProcessorService.GetAmountOfThousands);
        }

        private IList<string> ParseNumbers(int value, Language language, int minNumberToParse, Func<int, int> extractionFunc)
        {
            if (value < minNumberToParse)
            {
                return new List<string>();
            }

            var amountOfNumbers = extractionFunc(value);

            if (amountOfNumbers == 0)
            {
                return new List<string>();
            }

            var list = ProcessNumber(language, minNumberToParse, amountOfNumbers);

            return ProcessingLessThanMillion(minNumberToParse, language) && !_languageFeatureService.UsesSpacesBetweenNumbers(language)
                ? new List<string> { string.Concat(list) }
                : list;
        }

        private IList<string> ProcessNumber(Language language, int minNumberToParse, int amountOfNumbers)
        {
            var list = ParseFirstDigits(language, minNumberToParse, amountOfNumbers);

            if (ValuesOverMillionShouldBeConcatenated(language, minNumberToParse))
            {
                list = new List<string> { string.Concat(list) };
            }

            var number = _translationService.Translate(minNumberToParse, language);

            if (amountOfNumbers > 1 && _languageFeatureService.UsesPluralizedForms(language))
            {
                number += _languageFeatureService.GetPluralizedForm(language, number);
            }

            list.Add(number);

            return list;
        }

        private bool ValuesOverMillionShouldBeConcatenated(Language language, int minNumberToParse)
        {
            return minNumberToParse >= Constants.Million && !_languageFeatureService.UsesSpacesBetweenNumbers(language);
        }

        private IList<string> ParseFirstDigits(Language language, int minNumberToParse, int amountOfNumbers)
        {
            if (amountOfNumbers == 1)
            {
                if (!_languageFeatureService.SingleUnitIsSpecifiedAsADigit(language))
                {
                    return new List<string>();
                }

                else if (minNumberToParse >= Constants.Million && _languageFeatureService.UsesSpecialCaseForSingleUnitForMillionOrOver(language))
                {
                    return new List<string> { _languageFeatureService.GetSpecialCaseForSingleUnitForMillionOrOver(language) };
                }
            }

            return ParseDigits(language, amountOfNumbers);
        }

        private IList<string> ParseDigits(Language language, int amountOfNumbers)
        {
            var list = new List<string>();

            var threeDigitNumbers = ParseThreeDigitNumbers(amountOfNumbers, language);
            var twoDigitNumbers = ParseTwoDigitNumbers(amountOfNumbers, language);

            if (amountOfNumbers >= 100)
            {
                list.AddRange(threeDigitNumbers);
            }

            list.Add(twoDigitNumbers);

            return list;
        }

        private bool ProcessingLessThanMillion(int minNumberToParse, Language language)
        {
            return minNumberToParse < Constants.Million || !_languageFeatureService.UsesSpacesBetweenNumbersMillionAndOver(language);
        }

        public IList<string> ParseThreeDigitNumbers(int value, Language language)
        {
            var threeDigits = _numberProcessorService.GetThreeDigitNumber(value);
            var list = new List<string>();

            if (threeDigits >= 100)
            {
                list.Add(ParseThreeDigits(language, threeDigits));
            }

            return list;
        }

        private string ParseThreeDigits(Language language, int threeDigits)
        {
            var oneDigit = _numberProcessorService.GetFirstDigit(threeDigits);

            var result = string.Empty;
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

            return result;
        }

        public string ParseTwoDigitNumbers(int value, Language language)
        {
            var twoDigits = _numberProcessorService.GetTwoDigitNumber(value);

            if (twoDigits < 21 && twoDigits != 0)
            {
                return _translationService.Translate(twoDigits, language);
            }

            return ParseTwoDigitsOverTwenty(twoDigits, language);
        }

        private string ParseTwoDigitsOverTwenty(int twoDigits, Language language)
        {
            if (twoDigits > 20)
            {
                var evenTwoDigitNumber = _numberProcessorService.GetEvenTwoDigitNumber(twoDigits);

                var translation = _translationService.Translate(evenTwoDigitNumber, language);
                var secondDigit = AddSecondDigit(language, twoDigits);
                return translation + secondDigit;
            }

            return null;
        }

        private string AddSecondDigit(Language language, int twoDigits)
        {
            var lastDigit = _numberProcessorService.GetLastDigit(twoDigits);

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
