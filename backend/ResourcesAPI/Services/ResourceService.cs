using Microsoft.Extensions.Options;
using MongoDB.Driver;
using ResourcesAPI.Models;

namespace ResourcesAPI.Services;

public class ResourceService
{
    private readonly IMongoCollection<Resource> _resourceCollection;

    public ResourceService(IOptions<MongoDBSettings> mongoDBSettings)
    {
        var client = new MongoClient(mongoDBSettings.Value.ConnectionURI);
        var database = client.GetDatabase(mongoDBSettings.Value.DatabaseName);
        _resourceCollection = database.GetCollection<Resource>(mongoDBSettings.Value.CollectionName);
    }

    public async Task<IEnumerable<Resource>> GetAll() =>
        await _resourceCollection.Find(Builders<Resource>.Filter.Empty).ToListAsync();

    public async Task<Resource?> GetById(string id)
    {
        var filter = Builders<Resource>.Filter.Eq("Id", id);
        return await _resourceCollection.Find(filter).FirstOrDefaultAsync();
    }

    public async Task<Resource> Create(Resource resource) {
        await _resourceCollection.InsertOneAsync(resource);
        return resource;
    }

    public async Task<Resource> Update(string id, Resource resource)
    {
        var filter = Builders<Resource>.Filter.Eq("Id", id);
        await _resourceCollection.ReplaceOneAsync(filter, resource);
        return resource;
    }

    public async Task<bool> Delete(string id)
    {
        var filter = Builders<Resource>.Filter.Eq("Id", id);
        var result = await _resourceCollection.DeleteOneAsync(filter);
        return result.DeletedCount > 0;
    }
}
