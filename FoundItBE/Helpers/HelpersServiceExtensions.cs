using Dapper;
using FoundItBE.Models;

namespace FoundItBE.Helpers;


public static class HelpersServiceExtensions
{
    public static IServiceCollection AddHelpers(this IServiceCollection services)
    {
        SqlMapper.AddTypeHandler(typeof(Guid), new MySqlGuidTypeHandler());
        SqlMapper.AddTypeHandler(typeof(Guid?), new MySqlGuidTypeHandler());
        return services;
    }
}
