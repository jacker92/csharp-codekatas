namespace SocialNetwork.Console
{
    public interface IVerbLogic<T> where T : class
    {
        int Run(T options, string userName);
    }
}
