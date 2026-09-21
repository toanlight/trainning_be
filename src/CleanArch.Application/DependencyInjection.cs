using CleanArch.Application.Interfaces.Services;
using CleanArch.Application.Mappings;
using CleanArch.Application.Services;
using FluentValidation;
using Microsoft.Extensions.DependencyInjection;

namespace CleanArch.Application;

/// <summary>
/// Extension methods để đăng ký tất cả services của Application layer.
/// </summary>
public static class DependencyInjection
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        // AutoMapper — tự động scan tất cả profiles trong assembly này
        services.AddAutoMapper(typeof(ProductMappingProfile).Assembly);

        // FluentValidation — tự động scan tất cả validators trong assembly này
        services.AddValidatorsFromAssembly(typeof(DependencyInjection).Assembly);

        // Services
        services.AddScoped<IProductService, ProductService>();

        return services;
    }
}
