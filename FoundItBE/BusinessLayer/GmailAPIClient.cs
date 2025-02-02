using Google.Apis.Discovery.v1;
using Google.Apis.Services;
using Google.Apis.Gmail;
using Google.Apis.Gmail.v1;
namespace FoundItBE.BusinessLayer;

public static class GmailAPIClient
{
    public static async Task ExampleEmailHit()
    {
        var service = new GmailService(new BaseClientService.Initializer
        {
            ApplicationName = "FoundItBE",
            ApiKey = "thats_numberwang"
        });

        var result = service.Users.Settings.ForwardingAddresses.Get("me", "thats_numberwang");
        foreach (var item in service.Features)
        {
            Console.WriteLine(item);
        }
        var fullResult = result.UserId;

        Console.WriteLine(fullResult);
    }
}
