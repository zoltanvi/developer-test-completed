using Taxually.TechnicalTest.Clients;
using Taxually.TechnicalTest.Models.Requests;

namespace Taxually.TechnicalTest.Services.Strategies;

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
        await _httpClient.PostAsync(Url, request);
    }
}