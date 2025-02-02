using FoundItBE.Models;

namespace FoundItBE.Infrastructure;

public static class InfrastructureDomainExtensions
{
    public static IServiceCollection AddInstrstructure(this IServiceCollection services)
    {
        services.AddTransient<IDatabaseConnectionFactory<User>, MySqlConnection<User>>();
        services.AddTransient<IEmailConnectionFactory, GmailConnection>();
        return services;
    }
}
