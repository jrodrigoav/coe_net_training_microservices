namespace RentingAPI.DTO
{
    public class Renting
    {
        public Guid ID { get; set; }
        public Guid ClientID { get; set; }
        public Guid CopyID { get; set; }
        public DateTime ReturnDate { get; set; }
    }
}