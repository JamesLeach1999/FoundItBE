using Dapper;
using FoundItBE.Models;

namespace FoundItBE.Domain;

public static class DomainServiceExtensions
{
    public static IServiceCollection AddDomain(this IServiceCollection services)
    {
        services.AddTransient<IDatabaseConnectionFactory<User>, DatabaseConnectionFactory<User>>();
        services.AddTransient<IGetValues<User>, UserGetValuesSql>();
        services.AddTransient<ICreateValues<UserRequest, object>, UserPostValuesSql>();
        SqlMapper.AddTypeHandler(typeof(Guid), new MySqlGuidTypeHandler());
        SqlMapper.AddTypeHandler(typeof(Guid?), new MySqlGuidTypeHandler());

        return services;
    }
}
