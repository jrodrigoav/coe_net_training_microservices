using System.ComponentModel.DataAnnotations;

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
        public Guid CopyId { get; set; }
    }
}
