namespace InventoryAPI.DTO
{
    public class Inventory
    {
        public Guid ID { get; set; }
        public Guid ResourceID { get; set; }
        public bool Available { get; set; }
        public string Data { get; set; } = null!;
    }
}