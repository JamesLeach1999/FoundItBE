using FluentValidation;
using FinderBE.Models;

namespace FinderBE.Validation;

public static class ValidationServiceExtensions
{
    public static IServiceCollection AddValidation(this IServiceCollection services)
    {
        services.AddTransient<AbstractValidator<Guid>, UserRequestValidator>();
        services.AddTransient<AbstractValidator<User>, NewUserRequestValidator>();

        return services;
    }
}