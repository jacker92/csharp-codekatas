namespace NumbersToWords.Domain
{
    public interface ITranslationService
    {
        string Translate(int numberToTranslate, Language language = Language.English);
    }
}