using FinderBE.Models;

namespace FinderBE.Domain;

public static class DomainServiceExtensions
{
    public static IServiceCollection AddDomain(this IServiceCollection services)
    {
        services.AddTransient<IDatabaseConnectionFactory<User>, DatabaseConnectionFactory<User>>();
        services.AddTransient<IGetValues<User>, UserGetValuesSql>();
        services.AddTransient<ICreateValues<User, object>, UserPostValuesSql>();

        return services;
    }
}
