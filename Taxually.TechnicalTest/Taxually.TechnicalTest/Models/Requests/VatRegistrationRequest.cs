namespace Taxually.TechnicalTest.Models.Requests;

public record VatRegistrationRequest
{
    public string CompanyName { get; set; }
    public string CompanyId { get; set; }
    public string Country { get; set; }
}