using Microsoft.Extensions.Options;
using RentingAPI.Models;
using RentingAPI.Models.InventoryAPI;
using RentingAPI.Models.ResourcesAPI;

namespace RentingAPI.Services
{
    public class InventoryAPIClient : GenericAPIClient<InventoryAPISettings>
    {
        
        public InventoryAPIClient(HttpClient client, IOptionsMonitor<InventoryAPISettings> optionsMonitor) : base(client, optionsMonitor)
        {
            _client.BaseAddress = optionsMonitor.CurrentValue.Url;
        }

        public async Task<ItemResponse[]> ListResourceAvailabilityAsync(Guid resourceId)
        {
            var item = await _client.GetFromJsonAsync<ResourceResponse>($"{_client.BaseAddress}/{resourceId}");
            List<ItemResponse> r =new List<ItemResponse>();
            ItemResponse itemResponse =  new ItemResponse { Available = true ,ResourceId=item.Id,Id=item.Id,Name=item.Name};
            r.Add(itemResponse);
            return r.ToArray() ?? Array.Empty<ItemResponse>();
        }

        public async Task UpdateItemAvailabilityAsync(UpdateItemRequest updateItemRequest)
        {
            await _client.PutAsJsonAsync("setAvailability", updateItemRequest);
        }
    }


}
