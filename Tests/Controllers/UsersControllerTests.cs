using FoundItBE.Models;
using FoundItBE.ServiceHost;
using Microsoft.AspNetCore.Mvc.Testing;
using System.Text.Json;

namespace Tests.Controllers;
public class UsersControllerTests : IClassFixture<CustomWebAppFactory>
{
    private readonly CustomWebAppFactory _factory;

    public UsersControllerTests(CustomWebAppFactory factory)
    {
        _factory = factory;
    }

    [Fact]
    public async void Given_ACallToTheStubEndpoint_When_Executed_Then_StaticDataReturned()
    {
        var client = _factory.CreateClient();

        var response = await client.GetAsync("/Users/GetUsers");
        response.EnsureSuccessStatusCode();
        Assert.NotNull(response);

        var jsonString = await response.Content.ReadAsStringAsync();
        var responseJson = JsonSerializer.Deserialize<List<User>>(jsonString, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

        Assert.IsType<List<User>>(responseJson);
    }
}
