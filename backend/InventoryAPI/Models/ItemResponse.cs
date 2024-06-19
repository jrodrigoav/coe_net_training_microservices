namespace InventoryAPI.Models
{
    public class ItemResponse : ItemDTO
    {
        public Guid Id { get; init; }
        public bool Available { get; init; }
    }
}
