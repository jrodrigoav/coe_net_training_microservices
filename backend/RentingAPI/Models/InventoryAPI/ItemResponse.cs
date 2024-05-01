namespace RentingAPI.Models.InventoryAPI
{
    public class ItemResponse
    {
        public Guid Id { get; init; }
        public Guid ResourceId { get; init; }
        public bool Available { get; init; }
    }

    public class UpdateItemRequest
    {
        public Guid ItemId { get; init; }
        public bool Available { get; init; }
    }
}
