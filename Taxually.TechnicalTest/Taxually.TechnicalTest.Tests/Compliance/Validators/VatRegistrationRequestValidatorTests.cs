using Taxually.TechnicalTest.Compliance.Models.Requests;
using Taxually.TechnicalTest.Compliance.Validators;

namespace Taxually.TechnicalTest.Tests.Compliance.Validators;

public class VatRegistrationRequestValidatorTests
{
    private readonly VatRegistrationRequestValidator _validator;
    private const string ValidCompanyName = "asd";
    private const string ValidCompanyId = "123";
    private const string ValidCountry = "FR";

    public VatRegistrationRequestValidatorTests()
    {
        _validator = new VatRegistrationRequestValidator();
    }

    [Theory]
    [InlineData("fr")]
    [InlineData("FR")]
    [InlineData("fR")]
    [InlineData("Fr")]
    public void Validate_WithValidRequest_ReturnsNoErrors(string country)
    {
        var request = new VatRegistrationRequest
        {
            CompanyName = ValidCompanyName,
            CompanyId = ValidCompanyId,
            Country = country
        };

        var result = _validator.Validate(request);

        Assert.Empty(result);
    }

    [Theory]
    [InlineData(null)]
    [InlineData("")]
    [InlineData(" ")]
    public void Validate_WithNullCompanyName_ReturnsCompanyNameError(string companyName)
    {
        var request = new VatRegistrationRequest
        {
            CompanyName = companyName,
            CompanyId = ValidCompanyId,
            Country = ValidCountry
        };

        var result = _validator.Validate(request);

        Assert.Contains(VatRegistrationRequestValidator.CompanyNameMissingError, result);
    }

    [Theory]
    [InlineData(null)]
    [InlineData("")]
    [InlineData(" ")]
    public void Validate_WithNullCompanyId_ReturnsCompanyIdError(string companyId)
    {
        var request = new VatRegistrationRequest
        {
            CompanyName = ValidCompanyName,
            CompanyId = companyId,
            Country = ValidCountry
        };

        var result = _validator.Validate(request);

        Assert.Contains(VatRegistrationRequestValidator.CompanyIdMissingError, result);
    }

    [Theory]
    [InlineData(null)]
    [InlineData("")]
    [InlineData(" ")]
    public void Validate_WithNullCountry_ReturnsCountryError(string country)
    {
        var request = new VatRegistrationRequest
        {
            CompanyName = ValidCompanyName,
            CompanyId = ValidCompanyId,
            Country = country
        };

        var result = _validator.Validate(request);

        Assert.Contains(VatRegistrationRequestValidator.CountryMissingError, result);
    }

    [Theory]
    [InlineData("F")]
    [InlineData("FRA")]
    public void Validate_WithInvalidCountryCode_ReturnsLengthError(string invalidCountry)
    {
        var request = new VatRegistrationRequest
        {
            CompanyName = ValidCompanyName,
            CompanyId = ValidCompanyId,
            Country = invalidCountry
        };

        var result = _validator.Validate(request);

        Assert.Contains(VatRegistrationRequestValidator.CountryWrongFormatError, result);
    }

    [Fact]
    public void Validate_WithMultipleInvalidFields_ReturnsAllRelevantErrors()
    {
        var request = new VatRegistrationRequest
        {
            CompanyName = "",
            CompanyId = "   ",
            Country = "U"
        };

        var result = _validator.Validate(request);

        Assert.Contains(VatRegistrationRequestValidator.CompanyNameMissingError, result);
        Assert.Contains(VatRegistrationRequestValidator.CompanyIdMissingError, result);
        Assert.Contains(VatRegistrationRequestValidator.CountryWrongFormatError, result);
    }

    [Fact]
    public void Validate_WithNullRequest_ThrowsArgumentNullException()
    {
        Assert.Throws<ArgumentNullException>(() => _validator.Validate(null));
    }
}
