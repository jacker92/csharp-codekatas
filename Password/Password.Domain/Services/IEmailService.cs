namespace Password.Domain.Services
{
    public interface IEmailService
    {
        string SendEmail(string email);
    }
}