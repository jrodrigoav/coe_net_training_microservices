using System.Text.Json.Serialization;

namespace ClientsAPI.DTO
{
    //[DynamoDBTable("clients")]
    public class Client
    {
        //[DynamoDBHashKey]
        [JsonPropertyName("_id")]
        //[DynamoDBProperty("_id")]
        public string Id { get; set; } = null!;

        [JsonPropertyName("Name")]
        //[DynamoDBProperty("Name")]
        public string ClientName { get; set; } = null!;

        [JsonPropertyName("Email")]
        //[DynamoDBProperty("Email")]
        public string Email { get; set; } = null!;
    }
}
