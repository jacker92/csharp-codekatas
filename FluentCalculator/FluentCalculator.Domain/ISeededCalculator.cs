namespace FluentCalculator.Domain
{
    public interface ISeededCalculator
    {
        ISeededCalculator Plus(int toAdd);
        ISeededCalculator Redo();
        int Result();
        ISeededCalculator Undo();
        ISeededCalculator Minus(int toAdd);
        ISeededCalculator Save();
    }
}