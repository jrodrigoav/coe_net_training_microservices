namespace InventoryAPI.Models
{
    public class ResponseModel
    {
        public int AvailableCopies { get; set; }
        public int UnavailableCopies { get; set; }
        public int TotalCopies { get; set; }
    }
}
