namespace RentingAPI.Models
{
    public class FiltersModel
    {
        public bool expression { get; set; }
        public string attributeNames { get; set; } = null!;
        public bool attributeValues { get; set; }
    }
}
