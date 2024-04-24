namespace RentingAPI.Models.DTO;

public class RentingDTO
{
    public string? Id { get; set; }

    public string ResourceId { get; set; } = null!;

    public string? ResourceName { get; set; }

    public string ClientId { get; set; } = null!;

    public DateTime RegistrationDate { get; set; }

    public DateTime? ReturnDate { get; set; }

    public string? CopyId { get; set; }
}
