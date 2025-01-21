using FinderBE.Models;
using FinderBE.ServiceHost;
using Microsoft.AspNetCore.Mvc.Testing;
using System.Text.Json;

namespace Tests.Controllers;
public class UsersControllerTests : IClassFixture<WebApplicationFactory<Program>>
{
    private readonly HttpClient _client;

    public UsersControllerTests(WebApplicationFactory<Program> factory)
    {
        _client = factory.CreateClient();
    }

    [Fact]
    public async void Given_ACallToTheStubEndpoint_When_Executed_Then_StaticDataReturned()
    {
        var response = await _client.GetAsync("/Users/TestEndpoint");

        response.EnsureSuccessStatusCode();
        var jsonString = await response.Content.ReadAsStringAsync();
        var responseJson = JsonSerializer.Deserialize<User>(jsonString, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
        Assert.NotNull(responseJson.Username);
        Console.WriteLine(responseJson);
    }
}
