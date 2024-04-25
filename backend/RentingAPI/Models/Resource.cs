namespace RentingAPI.Models;

public class Resource
{
    public string? Id { get; set; }

    public string Name { get; set; } = null!;

    public DateTime DateOfPublication { get; set; }

    public string Author { get; set; } = null!;

    public List<string> Tags { get; set; } = [];

    public string Type { get; set; } = string.Empty;

    public string Description { get; set; } = null!;
}
