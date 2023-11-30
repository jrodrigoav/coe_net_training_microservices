namespace ResourcesAPI.Models
{
    public class ResourceModel
    {
        public string Name { get; set; } = null!;
        public string DateOfPublication { get; set; } = null!;
        public string Type { get; set; } = null!;
        public string Description { get; set; } = null!;
        public string[] Tags { get; set; } = null!;
    }
}
