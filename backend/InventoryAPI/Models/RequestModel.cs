namespace InventoryAPI.Models
{
    public class RequestModel
    {
        public string Id { get; set; } = null!;
        public bool IsAvailable { get; set; }
    }
}
