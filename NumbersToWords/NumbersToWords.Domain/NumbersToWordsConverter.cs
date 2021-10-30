﻿using NumbersToWords.Domain.Services;
using System;
using System.Collections.Generic;

namespace NumbersToWords.Domain
{
    public class NumbersToWordsConverter
    {
        private readonly NumberProcessor _numberProcessor;
        private readonly ITranslationService _translationService;
        private readonly ILanguageFeatureService _languageFeatureService;

        public NumbersToWordsConverter()
        {
            _translationService = new TranslationService();
            _numberProcessor = new NumberProcessor();
            _languageFeatureService = new LanguageFeatureService();
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
            values.AddRange(ParseThreeDigitNumbers(value, language));
            values.AddRange(ParseTwoDigitNumbers(value, language));

            return _languageFeatureService.UsesSpacesBetweenNumbers(language) ?
                string.Join(' ', values) :
                string.Concat(values);
        }

        private IList<string> ParseSevenEightAndNineDigitNumbers(int value, Language language)
        {
            var list = new List<string>();

            if (value >= 1000000)
            {
                var amountOfMillions = _numberProcessor.GetAmountOfMillions(value);
                if (amountOfMillions >= 100)
                {
                    list.AddRange(ParseThreeDigitNumbers(amountOfMillions, language));
                }

                list.AddRange(ParseTwoDigitNumbers(amountOfMillions, language));

                list.Add(_translationService.Translate(1000000, language));
            }

            return list;
        }

        private IList<string> ParseFourFiveAndSixDigitNumbers(int value, Language language)
        {
            var list = new List<string>();

            if (value >= 1000)
            {
                var amountOfThousands = _numberProcessor.GetAmountOfThousands(value);

                if (amountOfThousands == 0)
                {
                    return list;
                }

                if (amountOfThousands > 1 || _languageFeatureService.SingleUnitIsSpecifiedAsADigit(language))
                {
                    if (amountOfThousands >= 100)
                    {
                        list.AddRange(ParseThreeDigitNumbers(amountOfThousands, language));
                    }

                    list.AddRange(ParseTwoDigitNumbers(amountOfThousands, language));
                }

                var thousand = _translationService.Translate(1000, language);

                if (amountOfThousands > 1 && _languageFeatureService.UsesPluralizedForms(language))
                {
                    thousand += _languageFeatureService.GetPluralizedForm(language, value);
                }

                list.Add(thousand);
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
                    result += " ";
                }

                var hundred = _translationService.Translate(100, language);
                result += hundred;

                if (oneDigit != 1 && _languageFeatureService.UsesPluralizedForms(language))
                {
                    result += _languageFeatureService.GetPluralizedForm(language, 100);
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
            string digits = string.Empty;

            if (lastDigit == 0)
            {
                return digits;
            }

            if (_languageFeatureService.UsesDashes(language))
            {
                digits += "-";
            }

            digits += _translationService.Translate(lastDigit, language);

            return digits;
        }
    }
}
