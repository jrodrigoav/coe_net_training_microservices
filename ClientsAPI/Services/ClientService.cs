using ClientsAPI.Models;
using Microsoft.Extensions.Options;
using MongoDB.Bson;
using MongoDB.Driver;

namespace ClientsAPI.Services;

public class ClientService
{
    private readonly IMongoCollection<Client> _clientCollection;

    public ClientService(IOptions<MongoDBSettings> mongoDBSettings)
    {
        var client = new MongoClient(mongoDBSettings.Value.ConnectionURI);
        var database = client.GetDatabase(mongoDBSettings.Value.DatabaseName);
        _clientCollection = database.GetCollection<Client>(mongoDBSettings.Value.CollectionName);
    }

    public async Task<IEnumerable<Client>> GetAll()
    {
        return await _clientCollection.Find(Builders<Client>.Filter.Empty).ToListAsync();
    }

    public async Task<Client?> Get(string id)
    {
        var filter = Builders<Client>.Filter.Eq("Id", id);
        return await _clientCollection.Find(filter).FirstOrDefaultAsync();
    }

    public async Task<Client> Create(Client client)
    {
        await _clientCollection.InsertOneAsync(client);
        return client;
    }

    public async Task Update(string id, Client newClientData)
    {
        var filter = Builders<Client>.Filter.Eq("Id", id);
        await _clientCollection.ReplaceOneAsync(filter, newClientData);
    }
}
