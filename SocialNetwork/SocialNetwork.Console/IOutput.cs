namespace SocialNetwork.Console
{
    public interface IOutput
    {
        void WriteLine(string message);
        void WriteError(string message);
    }
}
