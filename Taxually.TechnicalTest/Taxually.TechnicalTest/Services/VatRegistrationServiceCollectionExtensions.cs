using Taxually.TechnicalTest.Services.Strategies;

namespace Taxually.TechnicalTest.Services;

public static class VatRegistrationServiceCollectionExtensions
{
    public static IServiceCollection AddVatRegistrationServices(this IServiceCollection services)
    {
        services.AddScoped<GbVatRegistrationStrategy>();
        services.AddScoped<FrVatRegistrationStrategy>();
        services.AddScoped<DeVatRegistrationStrategy>();

        services.AddScoped<IVatRegistrationStrategyResolver, VatRegistrationStrategyResolver>();

        return services;
    }
}
