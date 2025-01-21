using FoundItBE.Models;
using MySqlConnector;

using FoundItBE.Domain;
using Microsoft.Extensions.Configuration;

namespace Tests.Domain;
public class DatabaseConnectionFactoryTests : CustomWebAppFactory, IClassFixture<DatabaseFixture>
{
    private readonly IConfiguration _configuration;
    private readonly IDatabaseConnectionFactory<User> _sut;
    private readonly DatabaseFixture _fixture;

    public DatabaseConnectionFactoryTests(DatabaseFixture fixture)
    {
        _fixture = fixture;
        _sut = base.MySqlDatabaseFactory();
    }

    [Fact]
    public async Task Given_AValidConnection_When_Connecting_Then_ConnectionSucceeds()
    {
        var result = _sut.OpenConnection();

        Assert.IsType<MySqlConnection>(result);
    }

    [Fact]
    public async Task Given_AnAccessDeniedException_When_Thrown_Then_CorrectExceptionStatementThrown()
    {
        var invalidConfig = new Dictionary<string, string>
        {
            {"ConnectionStrings:DefaultConnection", "Server=127.0.0.1;Port=3307;User ID=numberwang;Password=numberwang;Database=users;AllowUserVariables=true;" }
        };

        var sut = base.MySqlDatabaseFactory(invalidConfig);

        var result = Assert.Throws<Exception>(() => sut.OpenConnection());

        Assert.Equal("Error establishing mysql connection: Access denied for this user", result.Message);
    }

    [Fact]
    public async Task Given_AnInvalidDatabase_When_Thrown_Then_CorrectExceptionStatementThrown()
    {
        var invalidConfig = new Dictionary<string, string>
        {
            {"ConnectionStrings:DefaultConnection", "Server=127.0.0.1;Port=3307;User ID=root;Password=jadlljames;Database=numberwang;AllowUserVariables=true;" }
        };

        var sut = base.MySqlDatabaseFactory(invalidConfig);

        var result = Assert.Throws<Exception>(() => sut.OpenConnection());

        Assert.Equal("Error establishing mysql connection: Database not found", result.Message);
    }
}
