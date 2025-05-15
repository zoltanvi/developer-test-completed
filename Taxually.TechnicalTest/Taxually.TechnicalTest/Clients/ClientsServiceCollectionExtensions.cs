namespace Taxually.TechnicalTest.Clients;

public static class ClientsServiceCollectionExtensions
{
    public static IServiceCollection AddClients(this IServiceCollection services)
    {
        services.AddScoped<ITaxuallyHttpClient, TaxuallyHttpClient>();
        services.AddScoped<ITaxuallyQueueClient, TaxuallyQueueClient>();

        return services;
    }
}
