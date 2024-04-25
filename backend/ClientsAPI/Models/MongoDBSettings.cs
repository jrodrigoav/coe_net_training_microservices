namespace ClientsAPI.Models;

public class MongoDBSettings
{
    public string ConnectionURI { get; init; } = null!;

    public string DatabaseName { get; init; } = null!;

    public string CollectionName { get; init; } = null!;
}
