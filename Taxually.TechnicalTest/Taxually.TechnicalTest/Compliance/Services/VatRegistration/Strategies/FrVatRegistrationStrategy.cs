using System.Text;
using Taxually.TechnicalTest.Clients;
using Taxually.TechnicalTest.Compliance.Models.Requests;

namespace Taxually.TechnicalTest.Compliance.Services.VatRegistration.Strategies;

public class FrVatRegistrationStrategy : IVatRegistrationStrategy
{
    private const string QueueName = "vat-registration-csv";
    private readonly ITaxuallyQueueClient _queueClient;

    public FrVatRegistrationStrategy(ITaxuallyQueueClient queueClient)
    {
        _queueClient = queueClient;
    }

    // France requires an excel spreadsheet to be uploaded to register for a VAT number
    public async Task RegisterAsync(VatRegistrationRequest request)
    {
        var csvBuilder = new StringBuilder();
        csvBuilder.AppendLine("CompanyName,CompanyId");
        csvBuilder.AppendLine($"{request.CompanyName},{request.CompanyId}");
        var csv = Encoding.UTF8.GetBytes(csvBuilder.ToString());

        // Log this action
        await _queueClient.EnqueueAsync(QueueName, csv);
    }
}
