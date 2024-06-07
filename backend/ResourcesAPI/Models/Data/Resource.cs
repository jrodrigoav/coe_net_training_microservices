
using System.ComponentModel.DataAnnotations;

namespace ResourcesAPI.Models.Data
{
    public class Resource
    {
        public Guid Id { get; set; }

        [Required, StringLength(254)]
        public string Name { get; set; } = null!;

        [DataType(DataType.Date)]
        public DateTime DateOfPublication { get; set; }

        [StringLength(254)]
        public string? Author { get; set; }

        [MaxLength(10)]
        public List<string> Tags { get; set; } = [];

        [StringLength(50)]
        public string Type { get; set; } = null!;

        [StringLength(500)]
        public string Description { get; set; } = null!;

        public int RentedItems { get; set; } = 0;   
        public int TotalItems { get; set; }=0;
    }
}