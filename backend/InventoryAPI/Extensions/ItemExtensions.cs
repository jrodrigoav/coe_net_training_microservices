using InventoryAPI.Models;
using InventoryAPI.Models.Data;

namespace InventoryAPI.Extensions
{
    public static class ItemExtensions
    {
        public static Item ToItem(this RegisterItemRequest request)
        {
            return new Item
            {
                ResourceId = request.ResourceId,
                Available = request.Available ?? true
            };
        }

        public static ItemResponse ToItemResponse(this Item item)
        {
            return new ItemResponse
            {
                Id = item.Id,
                Available = item.Available,
                ResourceId = item.ResourceId
            };
        }
    }


}
