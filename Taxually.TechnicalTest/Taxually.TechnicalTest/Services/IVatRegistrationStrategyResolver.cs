using Taxually.TechnicalTest.Models.Requests;
using Taxually.TechnicalTest.Services.Strategies;

namespace Taxually.TechnicalTest.Services;

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
