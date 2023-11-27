namespace RentingAPI.Models
{
    public class ResponseModel
    {
        public List<RentingModel> Items { get; set; } = null!;
        public int Count { get; set; }
    }
}
