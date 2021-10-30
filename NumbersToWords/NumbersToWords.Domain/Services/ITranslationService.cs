namespace NumbersToWords.Domain.Services
{
    public interface ITranslationService
    {
        string Translate(int numberToTranslate, Language language = Language.English);
    }
}