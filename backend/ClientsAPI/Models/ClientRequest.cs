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
}
