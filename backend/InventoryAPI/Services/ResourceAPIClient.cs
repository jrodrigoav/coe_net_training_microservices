using InventoryAPI.Models;
using InventoryAPI.Models.Data;
using Microsoft.Extensions.Options;

namespace InventoryAPI.Services;

public class ResourceAPIClient
{
    private readonly HttpClient _client;

    public ResourceAPIClient(HttpClient client, IOptionsMonitor<ResourceAPISettings> optionsMonitor)
    {
        _client = client;
        _client.BaseAddress = optionsMonitor.CurrentValue.Url;
    }

    public async Task<Resource[]> GetResourcesAsync()
    {
        var items = await _client.GetFromJsonAsync<Resource[]>("");
        return items ?? Array.Empty<Resource>();
    }

    public async Task<Resource?> Get(Guid resourceId)
    {
        return await _client.GetFromJsonAsync<Resource>($"{resourceId}");
    }
}
