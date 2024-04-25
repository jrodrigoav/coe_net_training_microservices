using System.ComponentModel.DataAnnotations;

namespace ResourcesAPI.Models
{
    public abstract class ResourceDTO
    {
        [Required, StringLength(254)]
        public string Name { get; init; } = null!;

        [DataType(DataType.Date)]
        public DateTime DateOfPublication { get; init; }

        [StringLength(254)]
        public string? Author { get; init; }

        [MaxLength(10)]
        public List<string> Tags { get; init; } = [];

        [StringLength(50)]
        public string Type { get; init; } = null!;

        [StringLength(500)]
        public string Description { get; init; } = null!;
    }
}