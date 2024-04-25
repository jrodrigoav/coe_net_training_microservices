using System.ComponentModel.DataAnnotations;

namespace ClientsAPI.Models
{
    public abstract class ClientRequest
    {
        [StringLength(150)]
        public string FirstName { get; init; } = null!;

        [StringLength(150)]
        public string LastName { get; init; } = null!;

    }

    public class CreateClientRequest : ClientRequest
    {
        [EmailAddress]
        public string Email { get; init; } = null!;

    }

    public class UpdateClientRequest : ClientRequest
    {
        [Required, StringLength(25)]
        public string Id { get; set; } = null!;
    }

    public class ClientResponse : ClientRequest
    {
        public string Id { get; set; } = null!;
    }
}
