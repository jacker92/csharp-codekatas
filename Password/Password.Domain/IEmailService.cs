namespace Password.Domain
{
    public interface IEmailService
    {
        string SendEmail(string email);
    }
}