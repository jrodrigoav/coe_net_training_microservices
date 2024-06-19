namespace RentingAPI.Models.ResourcesAPI
{
    public class ResourceResponse
    {
        public Guid Id { get; init; }
        public string Name { get; init; } = null!;

        public DateTime DateOfPublication { get; init; }

        public string? Author { get; init; }

        public List<string> Tags { get; init; } = [];

        public string Type { get; init; } = null!;

        public string Description { get; init; } = null!;
    }
}
