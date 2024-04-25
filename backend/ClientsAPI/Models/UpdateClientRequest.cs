using System.ComponentModel.DataAnnotations;

namespace ClientsAPI.Models
{
    public class UpdateClientRequest : ClientRequest
    {
        [Required, StringLength(25)]
        public Guid Id { get; init; }
    }
}
