using System.ComponentModel.DataAnnotations;

namespace ClientsAPI.Models.Data
{
    
    public class Client
    {
        public Guid Id { get; set; }

        [Required, StringLength(150)]
        public string FirstName { get; set; } = null!;

        [Required, StringLength(150)]
        public string LastName { get; set; } = null!;

        [Required, EmailAddress]
        public string Email { get; set; } = null!;
    }
}