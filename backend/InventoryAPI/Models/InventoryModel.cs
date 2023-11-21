using System.Text.Json.Serialization;

namespace InventoryAPI.Models
{
    public class InventoryModel
    {
        [JsonPropertyName("Id")]
        //[DynamoDBProperty("_id")]
        public string Id { get; set; } = null!;

        [JsonPropertyName("Name")]
        //[DynamoDBProperty("Name")]
        public string InventoryName { get; set; } = null!;
    }
}
