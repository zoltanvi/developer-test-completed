using Taxually.TechnicalTest.Models.Requests;
using Taxually.TechnicalTest.Services.Strategies;

namespace Taxually.TechnicalTest.Services;

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