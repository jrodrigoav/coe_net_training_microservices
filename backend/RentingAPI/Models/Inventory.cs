namespace RentingAPI.Models;

public class Inventory
{
    public string Id { get; set; } = null!;

    public bool Available { get; set; } = true;

    public string ResourceId { get; set; } = null!;
}
