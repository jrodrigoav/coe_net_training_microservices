using System.ComponentModel.DataAnnotations;

namespace RentingAPI.Models.DTO;

public class ReturnResourceDTO
{
    [Required]
    public DateTime ReturnDate { get; set; }
}
