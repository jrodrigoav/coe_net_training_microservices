using InventoryAPI.Models.Data;
using InventoryAPI.Services.Data;
using Microsoft.EntityFrameworkCore;

namespace InventoryAPI.Services;

public class InventoryService
{
    private readonly InventoryDbContext _inventoryDbContext;
    private readonly ResourceAPIClient _resourceAPIClient;

    public InventoryService(InventoryDbContext inventoryDbContext, ResourceAPIClient resourceAPIClient)
    {
        _inventoryDbContext = inventoryDbContext;
        _resourceAPIClient = resourceAPIClient;
    }

    public Item[] GetItems()
    {
        return _inventoryDbContext.Items.AsNoTracking().ToArray();
    }

    public Item? GetItemById(Guid itemId)
    {
        return _inventoryDbContext.Items.FindAsync(x => x.Id == itemId);
    }

    public async Task<Item> Create(Inventory inventory)
    {
        
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
