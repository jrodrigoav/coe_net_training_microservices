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
           //var baseUrl = "http://localhost:5183/api/resources";//not sure this is the correct url What is the business rule?
            var items = await _client.GetFromJsonAsync<ResourceResponse[]>($"{_client.BaseAddress}/{resourceId}");
            List<ItemResponse> r =new List<ItemResponse>();
            foreach (var item in items)
            {
                r.Add(new ItemResponse() { Id = item.Id, Available = true });
            }
            return r.ToArray() ?? Array.Empty<ItemResponse>();
        }

        public async Task UpdateItemAvailabilityAsync(UpdateItemRequest updateItemRequest)
        {
            await _client.PutAsJsonAsync("setAvailability", updateItemRequest);
        }
    }


}
