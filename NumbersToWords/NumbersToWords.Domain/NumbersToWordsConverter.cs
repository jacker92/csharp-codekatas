using System;
using System.Collections.Generic;

namespace NumbersToWords.Domain
{
    public class NumbersToWordsConverter
    {
        private readonly NumberProcessor _numberProcessor;
        private readonly ITranslationService _translationService;

        public NumbersToWordsConverter()
        {
            _translationService = new TranslationService();
            _numberProcessor = new NumberProcessor();
        }

        public string Convert(int value)
        {
            if (value < 0)
            {
                throw new ArgumentOutOfRangeException(nameof(value));
            }

            if (value == 0)
            {
                return _translationService.Translate(0);
            }

            var values = new List<string>();
            values.AddRange(ParseSevenEightAndNineDigitNumbers(value));
            values.AddRange(ParseFourFiveAndSixDigitNumbers(value));
            values.AddRange(ParseThreeDigitNumbers(value));
            values.AddRange(ParseTwoDigitNumbers(value));

            return string.Join(' ', values);
        }

        private IList<string> ParseSevenEightAndNineDigitNumbers(int value)
        {
            var list = new List<string>();

            if (value >= 1000000)
            {
                var amountOfMillions = _numberProcessor.GetAmountOfMillions(value);
                if (amountOfMillions >= 100)
                {
                    list.AddRange(ParseThreeDigitNumbers(amountOfMillions));
                }

                list.AddRange(ParseTwoDigitNumbers(amountOfMillions));

                list.Add($"{_translationService.Translate(1000000)}");
            }

            return list;
        }

        private IList<string> ParseFourFiveAndSixDigitNumbers(int value)
        {
            var list = new List<string>();

            if (value >= 1000)
            {
                var amountOfThousands = _numberProcessor.GetAmountOfThousands(value);

                if (amountOfThousands == 0)
                {
                    return list;
                }

                if (amountOfThousands >= 100)
                {
                    list.AddRange(ParseThreeDigitNumbers(amountOfThousands));
                }

                list.AddRange(ParseTwoDigitNumbers(amountOfThousands));

                list.Add($"{_translationService.Translate(1000)}");
            }

            return list;
        }

        private IList<string> ParseThreeDigitNumbers(int value)
        {
            var threeDigits = _numberProcessor.GetThreeDigitNumber(value);
            var list = new List<string>();

            if (threeDigits >= 100)
            {
                var oneDigit = _numberProcessor.GetFirstDigit(threeDigits);
                list.Add($"{_translationService.Translate(oneDigit)} {_translationService.Translate(100)}");
            }

            return list;
        }

        private IList<string> ParseTwoDigitNumbers(int value)
        {
            var twoDigits = _numberProcessor.GetTwoDigitNumber(value);
            var values = ParseTwoDigitsOverTwenty(twoDigits);

            if (twoDigits < 21 && twoDigits != 0)
            {
                values.Add(_translationService.Translate(twoDigits));
            }

            return values;
        }

        private List<string> ParseTwoDigitsOverTwenty(int twoDigits)
        {
            var values = new List<string>();

            if (twoDigits > 20)
            {
                var lastDigit = _numberProcessor.GetLastDigit(twoDigits);
                var evenTwoDigitNumber = _numberProcessor.GetEvenTwoDigitNumber(twoDigits);

                var evenTwoDigitNumberStr = _translationService.Translate(evenTwoDigitNumber);

                if (lastDigit != 0)
                {
                    evenTwoDigitNumberStr += $"-{_translationService.Translate(lastDigit)}";
                }

                values.Add(evenTwoDigitNumberStr);
            }

            return values;
        }
    }
}
