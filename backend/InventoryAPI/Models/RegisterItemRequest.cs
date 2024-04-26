namespace InventoryAPI.Models
{
    public abstract class ItemDTO
    {
        public Guid ResourceId { get; init; }
    }
    public class RegisterItemRequest : ItemDTO
    {
        public bool? Available { get; init; }
    }

    public class ItemResponse : ItemDTO
    {
        public Guid Id { get; init; }
        public bool Available { get; init; }
    }

    public class UpdateItemRequest
    {
        public Guid ItemId { get; init; }
        public bool Available { get; init; }
    }
}
