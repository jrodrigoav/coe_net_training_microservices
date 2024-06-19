namespace RentingAPI.Models.InventoryAPI
{
    public class ItemResponse
    {
        public Guid Id { get; init; }
        public Guid ResourceId { get; init; }
        public bool Available { get; init; }

        public string Name { get; init; }//TODO check if this is used
    }

    public class UpdateItemRequest
    {
        public Guid ItemId { get; init; }
        public bool Available { get; init; }
    }
}
