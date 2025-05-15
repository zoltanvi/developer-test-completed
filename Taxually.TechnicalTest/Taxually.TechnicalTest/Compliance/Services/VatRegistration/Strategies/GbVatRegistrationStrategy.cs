using Taxually.TechnicalTest.Clients;
using Taxually.TechnicalTest.Compliance.Models.Requests;

namespace Taxually.TechnicalTest.Compliance.Services.VatRegistration.Strategies;

public class GbVatRegistrationStrategy : IVatRegistrationStrategy
{
    private const string Url = "https://api.uktax.gov.uk";
    private readonly ITaxuallyHttpClient _httpClient;

    public GbVatRegistrationStrategy(ITaxuallyHttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    // UK has an API to register for a VAT number
    public async Task RegisterAsync(VatRegistrationRequest request)
    {
        // Log this action
        await _httpClient.PostAsync(Url, request);
    }
}