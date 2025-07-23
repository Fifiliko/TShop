using TShop.Application.Interfaces.Sevices;
using TShop.Application.Services;

public static class ServiceRegistration
{
    public static IServiceCollection AddApplicationServices(this IServiceCollection services)
    {
        services.AddScoped<IBrandService, BrandService>();
        return services;
    }
}


/// builder.Services.AddApplicationServices();

