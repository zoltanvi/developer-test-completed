using Taxually.TechnicalTest.Compliance.Models.Requests;
using Taxually.TechnicalTest.Compliance.Services.VatRegistration.Strategies;

namespace Taxually.TechnicalTest.Compliance.Services.VatRegistration;

internal class VatRegistrationStrategyResolver : IVatRegistrationStrategyResolver
{
    private readonly IServiceProvider _serviceProvider;

    public VatRegistrationStrategyResolver(IServiceProvider serviceProvider)
    {
        _serviceProvider = serviceProvider;
    }

    // Ignore the casing of request.Country
    public IVatRegistrationStrategy Resolve(VatRegistrationRequest request) => request.Country.ToUpper() switch
    {
        "GB" => _serviceProvider.GetRequiredService<GbVatRegistrationStrategy>(),
        "FR" => _serviceProvider.GetRequiredService<FrVatRegistrationStrategy>(),
        "DE" => _serviceProvider.GetRequiredService<DeVatRegistrationStrategy>(),
        _ => throw new NotSupportedException($"Country '{request.Country}' is not supported!")
    };
}