using Taxually.TechnicalTest.Compliance.Services.VatRegistration;

namespace Taxually.TechnicalTest.Compliance;

public static class ComplianceServiceCollectionExtensions
{
    public static IServiceCollection AddComplianceModule(this IServiceCollection services)
    {
        services.AddVatRegistrationServices();

        return services;
    }
}
