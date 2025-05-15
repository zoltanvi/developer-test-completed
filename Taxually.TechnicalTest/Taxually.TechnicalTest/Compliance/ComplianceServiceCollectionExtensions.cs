using Taxually.TechnicalTest.Compliance.Services.VatRegistration;
using Taxually.TechnicalTest.Compliance.Validators;

namespace Taxually.TechnicalTest.Compliance;

public static class ComplianceServiceCollectionExtensions
{
    public static IServiceCollection AddComplianceModule(this IServiceCollection services)
    {
        services.AddVatRegistrationServices();
        services.AddValidators();

        return services;
    }

    private static IServiceCollection AddValidators(this IServiceCollection services)
    {
        services.AddScoped<IVatRegistrationRequestValidator, VatRegistrationRequestValidator>();

        return services;
    }
}
