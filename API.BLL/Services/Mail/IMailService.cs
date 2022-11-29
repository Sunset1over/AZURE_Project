using MimeKit;

namespace API.BLL.Services.Mail
{
    public interface IMailService
    {
        public Task SendEmailAsync(string email, string subject, BodyBuilder builder);
    }
}
