using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;

namespace ResourcesAPI.Models;

public class Resource
{
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public string? Id { get; set; }

    public string Name { get; set; } = null!;

    public DateTime DateOfPublication { get; set; }

    public string Author { get; set; } = null!;

    public List<string> Tags { get; set; } = [];

    public string Type { get; set; } = null!;

    public string Description { get; set; } = null!;
}