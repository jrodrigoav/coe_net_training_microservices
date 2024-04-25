namespace InventoryAPI.Models;

public class InventorySummary
{
    public string ResourceId { get; set; } = null!;

    public string ResourceName { get; set; } = null!;

    public long AvailableCopies { get; set; }

    public long UnavailableCopies { get; set; }

    public long TotalCopies { get; set; }
}
