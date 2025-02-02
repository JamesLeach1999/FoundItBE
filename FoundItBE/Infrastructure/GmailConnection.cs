using FoundItBE.Infrastructure.InfrastructureOptions;
using Microsoft.Extensions.Options;
using System.Net;
using System.Net.Mail;

namespace FoundItBE.Infrastructure;

public class GmailConnection : IEmailConnectionFactory
{
    private IConfiguration _configuration;
    private GmailConnectionOptions _gmailOptions;

    public GmailConnection(IOptions<GmailConnectionOptions> gmailOptions)
    {
        _gmailOptions = gmailOptions.Value;
    }
    public SmtpClient GetEmailConnection()
    {
        using var smtpClient = new SmtpClient();

        smtpClient.Host = _gmailOptions.Host;
        smtpClient.Port = _gmailOptions.Port;
        smtpClient.Credentials = new NetworkCredential(_gmailOptions.Email, _gmailOptions.Password);

        smtpClient.EnableSsl = true;

        return smtpClient;
    }
}
