using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace InventoryAPI.Models;

public class Inventory
{
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public string? Id { get; set; }

    public bool Available { get; set; } = true;

    [BsonRepresentation(BsonType.ObjectId)]
    public string ResourceId { get; set; } = null!;
}
