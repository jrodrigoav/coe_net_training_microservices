namespace InventoryAPI.Models
{
    public class RegisterItemRequest : ItemDTO
    {
        public bool? Available { get; init; }
    }
}
