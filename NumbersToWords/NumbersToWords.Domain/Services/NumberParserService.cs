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

            return ProcessingLessThanMillion(minNumberToParse, language) && !_languageFeatureService.UsesSpacesBetweenNumbers(language) && list.Any()
                ? new List<string> { string.Concat(list) }
                : list;
        }

        private IList<string> ProcessNumber(Language language, int minNumberToParse, int amountOfNumbers)
        {
            var list = new List<string>();

            if (amountOfNumbers > 1 || _languageFeatureService.SingleUnitIsSpecifiedAsADigit(language))
            {
                if (minNumberToParse >= Constants.Million && _languageFeatureService.UsesSpecialCaseForSingleUnitForMillionOrOver(language) && amountOfNumbers == 1)
                {
                    list.Add(_languageFeatureService.GetSpecialCaseForSingleUnitForMillionOrOver(language));
                }
                else
                {
                    var threeDigitNumbers = ParseThreeDigitNumbers(amountOfNumbers, language);
                    var twoDigitNumbers = ParseTwoDigitNumbers(amountOfNumbers, language);

                    if (amountOfNumbers >= 100)
                    {
                        list.AddRange(threeDigitNumbers);
                    }

                    list.AddRange(twoDigitNumbers);
                }
            }

            if (minNumberToParse >= Constants.Million && _languageFeatureService.UsesSpacesBetweenNumbersMillionAndOver(language) && !_languageFeatureService.UsesSpacesBetweenNumbers(language))
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
                var oneDigit = _numberProcessorService.GetFirstDigit(threeDigits);

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

        public IList<string> ParseTwoDigitNumbers(int value, Language language)
        {
            var twoDigits = _numberProcessorService.GetTwoDigitNumber(value);
            var values = ParseTwoDigitsOverTwenty(twoDigits, language);

            if (twoDigits < 21 && twoDigits != 0)
            {
                values.Add(_translationService.Translate(twoDigits, language));
            }

            return values;
        }

        private IList<string> ParseTwoDigitsOverTwenty(int twoDigits, Language language)
        {
            var values = new List<string>();

            if (twoDigits > 20)
            {
                var lastDigit = _numberProcessorService.GetLastDigit(twoDigits);
                var evenTwoDigitNumber = _numberProcessorService.GetEvenTwoDigitNumber(twoDigits);

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
