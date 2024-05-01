namespace RentingAPI.Models
{
    public abstract class Rent
    {
        public Guid ResourceId { get; init; }
        public Guid ClientId { get; init; }
        public DateTime RegistrationDate { get; init; }
        public DateTime ReturnDate { get; init; }
    }
    public class RegisterRentRequest : Rent
    {

    }

    public class RentResponse : Rent
    {        
        public Guid Id { get; init; }        
        public Guid CopyId { get; init; }
    }
}
