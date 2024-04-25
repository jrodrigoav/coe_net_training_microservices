using System.ComponentModel.DataAnnotations;

namespace ClientsAPI.Models
{

    public class CreateClientRequest : ClientRequest
    {
        [EmailAddress]
        public string Email { get; init; } = null!;

    }
}
