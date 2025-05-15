using Taxually.TechnicalTest.Compliance.Models.Requests;

namespace Taxually.TechnicalTest.Compliance.Services.VatRegistration.Strategies;

public interface IVatRegistrationStrategy
{
    Task RegisterAsync(VatRegistrationRequest request);
}
