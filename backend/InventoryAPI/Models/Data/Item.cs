using System.ComponentModel.DataAnnotations;

namespace InventoryAPI.Models.Data
{
    public class Item
    {
        [Key]
        public Guid Id { get; set; }
        public bool Available { get; set; }
        public Guid ResourceId { get; set; }
        //public Guid ClientId { get; set; }
        //public bool Active { get; set; } = true; //to provide logical deletion
        
        //public DateTime RegistrationDate { get; set; }
        //public DateTime ReturnDate { get; set; }
    }
}