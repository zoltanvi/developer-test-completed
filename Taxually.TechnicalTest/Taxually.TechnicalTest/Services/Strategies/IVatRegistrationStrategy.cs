using Taxually.TechnicalTest.Models.Requests;

namespace Taxually.TechnicalTest.Services.Strategies;

public interface IVatRegistrationStrategy
{
    Task RegisterAsync(VatRegistrationRequest request);
}
