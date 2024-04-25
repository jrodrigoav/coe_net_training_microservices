namespace InventoryAPI.Models;

public class Resource
{
    public string Id { get; set; } = null!;

    public string Name { get; set; } = null!;

    public DateTime DateOfPublication { get; set; }

    public string? Author { get; set; }

    public List<string> Tags { get; set; } = [];

    public string Type { get; set; } = null!;

    public string Description { get; set; } = null!;
}
