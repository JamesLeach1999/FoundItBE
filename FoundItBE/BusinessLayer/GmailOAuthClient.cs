using Google.Apis.Auth.OAuth2;
using Google.Apis.Gmail.v1;
using Google.Apis.Services;
using Google.Apis.Util.Store;
using System.IO;
using System.Text;

namespace FoundItBE.BusinessLayer;

public static class GmailOAuthClient
{
    static string[] Scopes = { GmailService.Scope.GmailReadonly };
    static string ApplicationName = "FoundItBE";

    public static void InitialiseGmail()
    {
        try
        {
            UserCredential credentials;

            using var credentialsFile = new FileStream("C:\\Users\\jadll\\projects\\FoundItBE\\FoundItBE\\credentials.json", FileMode.Open, FileAccess.Read);

            var credPath = "token.json";

            credentials = GoogleWebAuthorizationBroker
                .AuthorizeAsync(GoogleClientSecrets.FromStream(credentialsFile).Secrets,
                Scopes,
                "user",
                CancellationToken.None,
                new FileDataStore(credPath, true)).Result;
            Console.WriteLine(credPath);

            var service = new GmailService(new BaseClientService.Initializer
            {
                HttpClientInitializer = credentials,
                ApplicationName = ApplicationName
            });

            UsersResource.LabelsResource.ListRequest request = service.Users.Labels.List("me");

            var labels = request.Execute().Labels;

            Console.WriteLine(labels);

            foreach (var labelItem in labels)
            {
                Console.WriteLine(labelItem);
            }

        }
        catch (FileNotFoundException ex) {
            Console.WriteLine(ex.Message);
        }
    }
}
