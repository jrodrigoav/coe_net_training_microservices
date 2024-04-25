using ClientsAPI.Extensions;
using ClientsAPI.Models;
using ClientsAPI.Models.Data;
using Microsoft.Extensions.Options;
using MongoDB.Bson;
using MongoDB.Driver;

namespace ClientsAPI.Services.Data;

public class ClientsService
{
    private readonly IMongoCollection<Client> _clientCollection;

    public ClientsService(IOptions<MongoDBSettings> mongoDBSettings)
    {
        var client = new MongoClient(mongoDBSettings.Value.ConnectionURI);
        var database = client.GetDatabase(mongoDBSettings.Value.DatabaseName);
        _clientCollection = database.GetCollection<Client>(mongoDBSettings.Value.CollectionName);
    }

    public async Task<IEnumerable<Client>> GetAllAsync()
    {
        return await _clientCollection.Find(Builders<Client>.Filter.Empty).ToListAsync();
    }

    public async Task<Client?> GetAsync(string clientId)
    {
        return await _clientCollection.Find(filter => filter.Id == clientId).FirstOrDefaultAsync();
    }

    public async Task<bool> UserEmailExistsAsync(string email)
    {
        return await _clientCollection.Find(filter => filter.Email == email).AnyAsync();
    }

    public async Task<Client> CreateAsync(CreateClientRequest createClientRequest)
    {
        await _clientCollection.InsertOneAsync(createClientRequest.ToClient());
        var results = await _clientCollection.FindAsync(filter => filter.FirstName == createClientRequest.FirstName && filter.LastName == createClientRequest.LastName && filter.Email == createClientRequest.Email);
        return results.First();
    }

    public async Task<Client?> DeleteAsync(string clientId)
    {
        return await _clientCollection.FindOneAndDeleteAsync(filter => filter.Id == clientId);
    }

    public async Task<Client> UpdateAsync(UpdateClientRequest updateClientRequest)
    {
        // Define your filter to find the document you want to update
        var filter = Builders<Client>.Filter.Eq(f => f.Id, updateClientRequest.Id);

        // Define the update operation
        var update = Builders<Client>.Update
            .Set(u => u.FirstName, updateClientRequest.FirstName)
            .Set(u => u.LastName, updateClientRequest.LastName);


        // Optionally, specify additional options (e.g., return the updated document)
        var options = new FindOneAndUpdateOptions<Client>
        {
            ReturnDocument = ReturnDocument.After // Return the updated document
        };

        return await _clientCollection.FindOneAndUpdateAsync(filter, update, options);
    }
}
