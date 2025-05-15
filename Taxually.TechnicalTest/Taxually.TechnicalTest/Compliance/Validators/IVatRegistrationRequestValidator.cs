using Taxually.TechnicalTest.Compliance.Models.Requests;

namespace Taxually.TechnicalTest.Compliance.Validators;

// We could use e.g. FluentValidation as a validator
public interface IVatRegistrationRequestValidator
{
    // For simplicity, the return type here is a string list that is empty if there is no error.
    List<string> Validate(VatRegistrationRequest request);
}
