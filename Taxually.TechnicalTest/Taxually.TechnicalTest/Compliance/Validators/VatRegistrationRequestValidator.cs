using Taxually.TechnicalTest.Compliance.Models.Requests;

namespace Taxually.TechnicalTest.Compliance.Validators;

public class VatRegistrationRequestValidator : IVatRegistrationRequestValidator
{
    public const string CompanyNameMissingError = $"{nameof(VatRegistrationRequest.CompanyName)} is required.";
    public const string CompanyIdMissingError = $"{nameof(VatRegistrationRequest.CompanyId)} is required.";
    public const string CountryMissingError = $"{nameof(VatRegistrationRequest.Country)} is required.";
    public const string CountryWrongFormatError = $"{nameof(VatRegistrationRequest.Country)} must be a 2-letter code.";

    public List<string> Validate(VatRegistrationRequest request)
    {
        // By default, asp.net core model binding creates an instance so this won't throw
        ArgumentNullException.ThrowIfNull(request);

        var errors = new List<string>();

        if (string.IsNullOrWhiteSpace(request.CompanyName))
        {
            errors.Add(CompanyNameMissingError);
        }

        if (string.IsNullOrWhiteSpace(request.CompanyId))
        {
            errors.Add(CompanyIdMissingError);
        }

        if (string.IsNullOrWhiteSpace(request.Country))
        {
            errors.Add(CountryMissingError);
        }
        else if (request.Country.Length != 2)
        {
            errors.Add(CountryWrongFormatError);
        }

        return errors;
    }
}