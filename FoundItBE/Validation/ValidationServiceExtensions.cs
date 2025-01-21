using FluentValidation;
using FoundItBE.Models;

namespace FoundItBE.Validation;

public static class ValidationServiceExtensions
{
    public static IServiceCollection AddValidation(this IServiceCollection services)
    {
        services.AddTransient<AbstractValidator<Guid>, UserRequestValidator>();
        services.AddTransient<AbstractValidator<UserRequest>, NewUserRequestValidator>();

        return services;
    }
}