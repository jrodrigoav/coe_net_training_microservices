
using Microsoft.Extensions.Options;
using MongoDB.Bson;
using MongoDB.Driver;
using RentingAPI.Models;

namespace RentingAPI.Services;

public class RentingService
{
    private readonly IMongoCollection<Renting> _rentingCollection;

    public RentingService(IOptions<MongoDBSettings> mongoDBSettings)
    {
        var client = new MongoClient(mongoDBSettings.Value.ConnectionURI);
        var database = client.GetDatabase(mongoDBSettings.Value.DatabaseName);
        _rentingCollection = database.GetCollection<Renting>(mongoDBSettings.Value.CollectionName);
    }

    public async Task<IEnumerable<Renting>> GetAllRents()
    {
        return await _rentingCollection.Find(Builders<Renting>.Filter.Empty).ToListAsync();
    }

    public async Task<Renting> Get(string id)
    {
        var filter = Builders<Renting>.Filter.Eq("Id", id);
        return await _rentingCollection.Find(filter).FirstAsync();
    }

    public async Task<IEnumerable<Renting>> GetRentsByClient(string clientId, bool returned)
    {
        var clientFilter = Builders<Renting>.Filter.Eq("ClientId", clientId);
        var returnedFilter = Builders<Renting>.Filter.Eq("Returned", returned);
        var filter = Builders<Renting>.Filter.And(clientFilter, returnedFilter);
        return await _rentingCollection.Find(filter).ToListAsync();
    }

    public async Task<Renting> CreateRentForResource(Renting renting)
    {
        await _rentingCollection.InsertOneAsync(renting);
        return renting;
    }

    public async Task<bool> SetAsReturned(string id, DateTime returnDate)
    {
        var filter = Builders<Renting>.Filter.Eq("Id", id);
        var update = Builders<Renting>.Update.Set("ReturnDate", returnDate).Set("Returned", true);
        var updateResult = await _rentingCollection.UpdateOneAsync(filter, update);
        return updateResult.MatchedCount == 1;
    }
}
