using System.ComponentModel.DataAnnotations;

namespace InventoryAPI.Models.Data
{
    public class Item
    {
        [Key]
        public Guid Id { get; set; }
        public bool Available { get; set; }
        public Guid ResourceId { get; set; }
    }
}