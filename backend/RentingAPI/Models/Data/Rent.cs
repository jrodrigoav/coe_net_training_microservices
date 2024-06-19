using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RentingAPI.Models.Data
{
    public class Rent
    {
        [Key]
        public Guid Id { get; set; }
        public Guid ResourceId { get; set; }
        public Guid ClientId { get; set; }

        public DateTime RegistrationDate { get; set; }
        
        public DateTime ReturnDate { get; set; }
        public bool Returned { get; set; }
        public Guid CopyId { get; set; }

        [NotMapped]
        public string Name { get; set; }


        [NotMapped]
        public string ResourceName { get; set; }
    }
}
