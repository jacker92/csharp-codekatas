namespace CharacterCopy.Domain
{
    public interface IDestination
    {
        void WriteChar(char c);
        void WriteChars(char[] chars);
    }
}
