using System.Net.Mail;

namespace FoundItBE.Infrastructure;

public interface IEmailConnectionFactory
{
    public SmtpClient GetEmailConnection();
}
