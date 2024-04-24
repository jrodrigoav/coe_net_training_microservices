using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;

namespace RentingAPI.Models;

public class Renting
{
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public string? Id { get; set; }

    [BsonRepresentation(BsonType.ObjectId)]
    public string ResourceId { get; set; } = null!;

    [BsonRepresentation(BsonType.ObjectId)]
    public string ClientId { get; set; } = null!;

    public DateTime RegistrationDate { get; set; }

    public DateTime? ReturnDate { get; set; }

    public bool Returned { get; set; }

    [BsonRepresentation(BsonType.ObjectId)]
    public string? CopyId { get; set; }
}
