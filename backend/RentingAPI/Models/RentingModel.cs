namespace RentingAPI.Models
{
    public class RentingModel
    {
        public string ResourceId { get; set; } = null!;
        public string ClientId { get; set; } = null!;
        public string RegistrationDate { get; set; } = null!;
        public string ReturnDate { get; set; } = null!;
        public string CopyId { get; set; } = null!;
        public string ResourceName { get; set; } = null!;
    }
}
