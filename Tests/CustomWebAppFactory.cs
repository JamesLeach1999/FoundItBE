using DotNet.Testcontainers.Builders;
using Microsoft.AspNetCore.Mvc.Testing;
using System.Diagnostics;
using FoundItBE.ServiceHost;
using TestContainers;
using FoundItBE.Models;
using Testcontainers.MySql;
using FoundItBE.Domain;
using AutoFixture;
using NSubstitute;
using Microsoft.Extensions.Configuration;
using Dapper;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using FoundItBE.Validation;
using MySql.Data.MySqlClient;
using System.Data;

namespace Tests;

public class CustomWebAppFactory : WebApplicationFactory<Program>, IAsyncLifetime
{
    private static readonly Dictionary<string, string> defaultConfiguration = new Dictionary<string, string>() { { "ConnectionStrings:DefaultConnection", "Server=127.0.0.1;Port=3307;User ID=root;Password=jadlljames;Database=tests;AllowUserVariables=true;" } };

    public CustomWebAppFactory()
    {
        _ = Container;
    }

    public static readonly Lazy<MySqlContainer> LazyContainer = new Lazy<MySqlContainer>(() =>
    {
        var mysqlContainer = new MySqlBuilder()
           .WithImage("mysql")
           .WithExposedPort(3306)
           .WithPortBinding(3307, 3306)
           .WithEnvironment("MYSQL_ROOT_PASSWORD", "jadlljames")
           .WithEnvironment("MYSQL_DATABASE", "tests")
           .WithVolumeMount("founditbe_mysql-data", "/var/lib/mysql")
           .WithNetwork($"my-network")
           .WithWaitStrategy(Wait.ForUnixContainer()
            .UntilMessageIsLogged("ready for connections")
            .UntilPortIsAvailable(3306))
           .WithCleanUp(false)
           .Build();

        mysqlContainer.StartAsync().Wait();
        return mysqlContainer;
    });
    public static MySqlContainer Container => LazyContainer.Value;

    protected override void ConfigureWebHost(IWebHostBuilder builder)
    {
        builder.ConfigureTestServices(services =>
        {
            services.AddValidation();
            services.AddDomain();
        });
    }


    public async Task InitializeAsync()
    {
        SqlMapper.AddTypeHandler(typeof(Guid), new MySqlGuidTypeHandler());
        SqlMapper.AddTypeHandler(typeof(Guid?), new MySqlGuidTypeHandler());
    }

    public Task DisposeAsync()
    {
        return Task.CompletedTask;
    }



    public IDatabaseConnectionFactory<User> MySqlDatabaseFactory(Dictionary<string, string>? testConfig = null)
    {
        var config = new ConfigurationBuilder().AddInMemoryCollection(testConfig ?? defaultConfiguration).Build();
        
        return new DatabaseConnectionFactory<User>(config);
    }

}
