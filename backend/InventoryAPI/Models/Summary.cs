namespace InventoryAPI.Models
{
    public class Summary
    {
        public Guid ResourceId { get; init; }
        public string ResourceName { get; init; } = null!;
        public int AvailableCopies { get; init; }
        public int UnavailableCopies { get; init; }
        public int TotalCopies { get; init; }
    }
}
