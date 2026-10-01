using Microsoft.Azure.Cosmos;
using SupportWebApp.Models;

namespace SupportWebApp.Services;

public class CosmosDbService
{
    private readonly Container _container;

    public CosmosDbService(IConfiguration configuration)
    {
        var connectionString = configuration["CosmosDb:ConnectionString"];

        var databaseName = configuration["CosmosDb:DatabaseName"];

        var containerName = configuration["CosmosDb:ContainerName"];

        var client = new CosmosClient(connectionString);

        _container = client.GetContainer(databaseName, containerName);
    }

    public async Task AddSupportMessageAsync(SupportMessage message)
    {
        await _container.CreateItemAsync(
            message,
            new PartitionKey(message.Category));
    }

    public async Task<List<SupportMessage>> GetSupportMessagesAsync()
    {
        var messages = new List<SupportMessage>();

        var query = _container.GetItemQueryIterator<SupportMessage>(
            "SELECT * FROM c");

        while (query.HasMoreResults)
        {
            var response = await query.ReadNextAsync();
            messages.AddRange(response);
        }

        return messages;
    }
}