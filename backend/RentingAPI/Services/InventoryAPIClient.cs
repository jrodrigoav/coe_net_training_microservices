using Microsoft.Extensions.Options;
using RentingAPI.Models;
using RentingAPI.Models.InventoryAPI;

namespace RentingAPI.Services
{
    public class InventoryAPIClient : GenericAPIClient<InventoryAPISettings>
    {
        private readonly ResourcesAPIClient _resourcesAPIClient;

        public InventoryAPIClient(HttpClient client, IOptionsMonitor<InventoryAPISettings> optionsMonitor, ResourcesAPIClient resourcesAPIClient) : base(client, optionsMonitor)
        {
            _client.BaseAddress = optionsMonitor.CurrentValue.Url;
            _resourcesAPIClient = resourcesAPIClient;
        }

        public async Task<ItemResponse[]> ListResourceAvailabilityAsync(Guid resourceId)
        {
            var resource = await _resourcesAPIClient.GetResourceByIdAsync(resourceId);
            if (resource == null)
            {
                return Array.Empty<ItemResponse>();
            }

            var availableItems = new List<ItemResponse>();

            await foreach (var item in _client.GetFromJsonAsAsyncEnumerable<ItemResponse>($"{_client.BaseAddress}/list/{resourceId}") ?? AsyncEnumerable.Empty<ItemResponse>())
            {
                availableItems.Add(new ItemResponse
                {
                    Id = item.Id,
                    Available = true,
                    ResourceId = resource.Id,
                    Name = resource.Name
                });
            }

            return availableItems.ToArray();
        }

        public async Task UpdateItemAvailabilityAsync(UpdateItemRequest updateItemRequest)
        {
            await _client.PutAsJsonAsync($"{_client.BaseAddress}/setAvailability", updateItemRequest);
        }
    }


}
