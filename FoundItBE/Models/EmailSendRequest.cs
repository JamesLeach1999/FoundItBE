namespace FoundItBE.Models;

public class EmailSendRequest
{
    public string Recipient { get; set; }
    public string Sender { get; set; }
    public string Subject { get; set; }
}
