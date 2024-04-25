using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;

namespace ClientsAPI.Models;

public class Client
{
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public string? Id { get; set; }

    public string Name { get; set; } = string.Empty;

    public string Email { get; set; } = string.Empty;
}
