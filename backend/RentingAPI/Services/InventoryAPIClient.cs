using Microsoft.Extensions.Options;
using RentingAPI.Models;
using RentingAPI.Models.InventoryAPI;

namespace RentingAPI.Services
{
    public class InventoryAPIClient : GenericAPIClient<InventoryAPISettings>
    {
        public InventoryAPIClient(HttpClient client, IOptionsMonitor<InventoryAPISettings> optionsMonitor) : base(client, optionsMonitor)
        {

        }

        public async Task<ItemResponse[]> ListResourceAvailabilityAsync(Guid resourceId)
        {
            var items = await _client.GetFromJsonAsync<ItemResponse[]>($"list/{resourceId}");
            return items ?? Array.Empty<ItemResponse>();
        }

        public async Task UpdateItemAvailabilityAsync(UpdateItemRequest updateItemRequest)
        {
            await _client.PutAsJsonAsync("setAvailability", updateItemRequest);
        }
    }


}
