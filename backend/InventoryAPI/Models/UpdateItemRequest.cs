namespace InventoryAPI.Models
{
    public class UpdateItemRequest
    {
        public Guid ItemId { get; init; }
        public bool Available { get; init; }
    }
}
