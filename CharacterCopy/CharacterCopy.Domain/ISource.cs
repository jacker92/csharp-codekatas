namespace CharacterCopy.Domain
{
    public interface ISource
    {
        char ReadChar();
        char[] ReadChars(int count);
    }
}
