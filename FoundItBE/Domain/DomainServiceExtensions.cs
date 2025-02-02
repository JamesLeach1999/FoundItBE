using FoundItBE.Models;

namespace FoundItBE.Domain;

public static class DomainServiceExtensions
{
    public static IServiceCollection AddDomain(this IServiceCollection services)
    {
        services.AddTransient<IGetValues<User>, UserGetValuesSql>();
        services.AddTransient<ICreateValues<UserRequest, object>, UserPostValuesSql>();

        return services;
    }
}
