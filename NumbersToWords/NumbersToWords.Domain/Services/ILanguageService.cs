using NumbersToWords.Domain.Languages;
using System.Collections.Generic;

namespace NumbersToWords.Domain.Services
{
    public interface ILanguageService
    {
        IList<ILanguage> Languages { get; }
    }
}
