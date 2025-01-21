using FinderBE.Models;

namespace FinderBE.Helpers;


public static class HelpersServiceExtensions
{
    public static IServiceCollection AddHelpers(this IServiceCollection services)
    {
        services.AddTransient<ICustomOrm<User>, CustomOrm<User>>();

        return services;
    }
}
