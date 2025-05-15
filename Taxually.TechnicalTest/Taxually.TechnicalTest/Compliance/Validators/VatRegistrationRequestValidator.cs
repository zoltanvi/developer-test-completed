using Taxually.TechnicalTest.Compliance.Models.Requests;

namespace Taxually.TechnicalTest.Compliance.Validators;

public class VatRegistrationRequestValidator : IVatRegistrationRequestValidator
{
    public List<string> Validate(VatRegistrationRequest request)
    {
        // By default, asp.net core model binding creates an instance so this won't throw
        ArgumentNullException.ThrowIfNull(request);

        var errors = new List<string>();

        if (string.IsNullOrWhiteSpace(request.CompanyName))
        {
            errors.Add($"{nameof(request.CompanyName)} is required.");
        }

        if (string.IsNullOrWhiteSpace(request.CompanyId))
        {
            errors.Add($"{nameof(request.CompanyId)} is required.");
        }

        if (string.IsNullOrWhiteSpace(request.Country))
        {
            errors.Add($"{nameof(request.Country)} is required.");
        }
        else if (request.Country.Length != 2)
        {
            errors.Add("Country must be a 2-letter code.");
        }

        return errors;
    }
}