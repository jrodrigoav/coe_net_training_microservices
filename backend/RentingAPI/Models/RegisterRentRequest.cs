using System.Runtime.InteropServices;

namespace RentingAPI.Models
{
    public abstract class Rent
    {
        public Guid ResourceId { get; init; }
        public Guid ClientId { get; init; }
        public DateTime RegistrationDate { get; init; }
        public DateTime ReturnDate { get; init; }
        public bool Returned { get; init; }

        public string? ResourceName { get; init; }
    }
    public class RegisterRentRequest : Rent
    {

    }

    public class ReturnRent
    {
        public Guid Id { get; init; }
        public DateTime ReturnDate { get; init; }
    }

    public class RentResponse : Rent
    {        
        public Guid Id { get; init; }        
        public Guid CopyId { get; init; }
        public string? ResourceName { get; set; }
    }
}
