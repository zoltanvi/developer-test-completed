using System.Xml.Serialization;
using Taxually.TechnicalTest.Clients;
using Taxually.TechnicalTest.Compliance.Models.Requests;

namespace Taxually.TechnicalTest.Compliance.Services.VatRegistration.Strategies;

public class DeVatRegistrationStrategy : IVatRegistrationStrategy
{
    private const string QueueName = "vat-registration-xml";
    private readonly ITaxuallyQueueClient _queueClient;

    public DeVatRegistrationStrategy(ITaxuallyQueueClient queueClient)
    {
        _queueClient = queueClient;
    }

    // Germany requires an XML document to be uploaded to register for a VAT number
    public async Task RegisterAsync(VatRegistrationRequest request)
    {
        using var stringWriter = new StringWriter();
        var serializer = new XmlSerializer(typeof(VatRegistrationRequest));
        serializer.Serialize(stringWriter, request);
        var xml = stringWriter.ToString();
        await _queueClient.EnqueueAsync(QueueName, xml);
    }
}