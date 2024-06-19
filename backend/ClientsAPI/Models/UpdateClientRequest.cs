using System.ComponentModel.DataAnnotations;

namespace ClientsAPI.Models
{
    public class UpdateClientRequest : ClientRequest
    {
        //[Required, StringLength(36)]
        //public String Id { get; init; }


        [Required]
        public Guid Id { get; init; }
    }
}
