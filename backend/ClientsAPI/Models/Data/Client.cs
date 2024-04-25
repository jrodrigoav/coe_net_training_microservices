using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;
using System.ComponentModel.DataAnnotations;

namespace ClientsAPI.Models.Data
{
    public class Client
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string? Id { get; set; }

        [StringLength(150)]
        public string FirstName { get; init; } = null!;

        [StringLength(150)]
        public string LastName { get; init; } = null!;

        [EmailAddress]
        public string Email { get; init; } = null!;
    }
}