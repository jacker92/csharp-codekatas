namespace SocialNetwork.Console.VerbLogics
{
    public interface IVerbLogic<T> where T : class
    {
        int Run(T options, string userName);
    }
}
