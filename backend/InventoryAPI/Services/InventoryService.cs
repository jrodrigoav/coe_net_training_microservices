using InventoryAPI.Models;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace InventoryAPI.Services;

public class InventoryService
{
    private readonly IMongoCollection<Inventory> _inventoryCollection;

    public InventoryService(IOptions<MongoDBSettings> mongoDBSettings)
    {
        var client = new MongoClient(mongoDBSettings.Value.ConnectionURI);
        var database = client.GetDatabase(mongoDBSettings.Value.DatabaseName);
        _inventoryCollection = database.GetCollection<Inventory>(mongoDBSettings.Value.CollectionName);
    }

    public async Task<IEnumerable<Inventory>> GetAll() =>
        await _inventoryCollection.Find(Builders<Inventory>.Filter.Empty).ToListAsync();

    public async Task<Inventory?> Get(string id)
    {
        var filter = Builders<Inventory>.Filter.Eq("Id", id);
        return await _inventoryCollection.Find(filter).FirstAsync();
    }

    public async Task<Inventory> Create(Inventory inventory)
    {
        await _inventoryCollection.InsertOneAsync(inventory);
        return inventory;
    }

    public async Task<bool> Update(string id, Inventory inventory)
    {
        var filter = Builders<Inventory>.Filter.Eq("Id", id);
        var result = await _inventoryCollection.ReplaceOneAsync(filter, inventory);
        return result.ModifiedCount > 0;
    }

    public async Task<bool> Delete(string id)
    {
        var filter = Builders<Inventory>.Filter.Eq("Id", id);
        var result = await _inventoryCollection.DeleteOneAsync(filter);
        return result.DeletedCount > 0;
    }

    public async Task<IEnumerable<Inventory>> GetByResourceId(string resourceId, bool available)
    {
        var resourceIdFilter = Builders<Inventory>.Filter.Eq("ResourceId", resourceId);
        var availabilityFilter = Builders<Inventory>.Filter.Eq("Available", available);
        var filter = Builders<Inventory>.Filter.And(resourceIdFilter, availabilityFilter);
        return await _inventoryCollection.Find(filter).ToListAsync();
    }

    public async Task<long> GetCountByResourceId(string resourceId, bool available)
    {
        var resourceIdFilter = Builders<Inventory>.Filter.Eq("ResourceId", resourceId);
        var availabilityFilter = Builders<Inventory>.Filter.Eq("Available", available);
        var filter = Builders<Inventory>.Filter.And(resourceIdFilter, availabilityFilter);
        return await _inventoryCollection.CountDocumentsAsync(filter);
    }
}
