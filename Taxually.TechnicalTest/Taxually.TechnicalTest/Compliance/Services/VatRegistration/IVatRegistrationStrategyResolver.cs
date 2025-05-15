using Taxually.TechnicalTest.Compliance.Models.Requests;
using Taxually.TechnicalTest.Compliance.Services.VatRegistration.Strategies;

namespace Taxually.TechnicalTest.Compliance.Services.VatRegistration;

public interface IVatRegistrationStrategyResolver
{
    /// <summary>
    /// Resolves the appropriate VAT registration strategy based on the request details.
    /// </summary>
    /// <param name="request">The VAT registration request.</param>
    /// <returns>The <see cref="IVatRegistrationStrategy"/> to handle the request.</returns>
    /// <exception cref="NotSupportedException">When the request contains a country that is not supported for VAT registration.</exception>
    IVatRegistrationStrategy Resolve(VatRegistrationRequest request);
}
