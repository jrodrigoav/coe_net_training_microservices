using InventoryAPI.Models;
using Microsoft.Extensions.Options;

namespace InventoryAPI.Services
{

    public class ResourceAPIClient
    {
        private readonly HttpClient _client;

        public ResourceAPIClient(HttpClient client, IOptionsMonitor<ResourceAPISettings> optionsMonitor)
        {
            _client = client;
            _client.BaseAddress = optionsMonitor.CurrentValue.Url;
        }

        public async Task<Resource[]> ListResourcesAsync()
        {
            var items = await _client.GetFromJsonAsync<Resource[]>(_client.BaseAddress+ "/list");
            return items ?? Array.Empty<Resource>();
        }

        public async Task<Resource?> GetByResourceIdAsync(Guid resourceId)
        {
            return await _client.GetFromJsonAsync<Resource>($"{_client.BaseAddress}/{resourceId}");
        }
    }
}