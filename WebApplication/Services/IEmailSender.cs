namespace WebApplication.Services
{
    public interface IEmailSender
    {
        public Task SendEmail(string recipient, string subject, string message);
    }
}
