using FoundItBE.Domain;
using FoundItBE.Infrastructure;
using FoundItBE.Models;
using Microsoft.Extensions.Configuration;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NSubstitute;
using NSubstitute.ExceptionExtensions;
using Assert = Xunit.Assert;

namespace Tests.Domain;
public class UserGetValuesSqlTests : CustomWebAppFactory, IClassFixture<DatabaseFixture>
{
    private readonly IGetValues<User> _sut;
    private readonly DatabaseFixture _fixture;

    public UserGetValuesSqlTests(DatabaseFixture fixture)
    {
        _fixture = fixture;
        var databaseConnectionFactory = base.MySqlDatabaseFactory();

        _sut = new UserGetValuesSql(databaseConnectionFactory);
    }

    [Fact]
    public async Task Given_AValidDatabaseConnection_When_GetValuesCalled_Then_ReturnListOfUsers()
    {
        var result = await _sut.GetValues();
        Console.WriteLine(result);
        Assert.IsType<List<User>>(result);
    }

    [Fact]
    public async Task Given_AValidUserId_When_GetValueCalled_Then_ReturnSingleUser()
    {
        var result = await _sut.GetValue(new Guid("dd0c3464-c7ad-435f-8b9e-4679f9d2c47d"));

        Assert.IsType<User>(result);
    }

    [Fact]
    public async Task Given_ANonExistantUserId_When_GetValueCalled_Then_ReturnNull()
    {
        var nonexistantUserId = new Guid("3d91fbd8-adaa-4f31-ade9-c0e82a7e099e");

        var result = await _sut.GetValue(nonexistantUserId);

        Assert.Null(result);
    }

    [Fact]
    public async Task Given_AInvalidConnection_When_GetValueCalled_Then_ExceptionThrown()
    {
        var stubDbFactory = Substitute.For<IDatabaseConnectionFactory<User>>();

        var sut = new UserGetValuesSql(stubDbFactory);

        var result = await Assert.ThrowsAsync<Exception>(() => sut.GetValue(new Guid("3d91fbd8-adaa-4f31-ade9-c0e82a7e099f")));

        Assert.Equal("Error getting single user", result.Message);

    }
}
