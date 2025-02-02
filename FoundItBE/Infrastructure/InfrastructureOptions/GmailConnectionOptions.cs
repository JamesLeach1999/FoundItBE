namespace FoundItBE.Infrastructure.InfrastructureOptions;

public class GmailConnectionOptions
{
    public const string GmailOptionsKey = "GmailOptions";

    public string Host { get; set; }
    public string Password { get; set; }
    public string Email { get; set; }
    public int Port { get; set; }
}
