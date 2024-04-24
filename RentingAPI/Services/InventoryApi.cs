using RentingAPI.Models;

namespace RentingAPI.Services;

public class InventoryApi(HttpClient httpClient)
{
    public async Task<IEnumerable<Inventory>> GetResourceInventory(string resourceId)
    {
        return await httpClient.GetFromJsonAsync<IEnumerable<Inventory>>($"resource/{resourceId}") ?? throw new NullReferenceException($"Failed to retrieve inventory for resource {resourceId}");
    }

    public async Task<bool> SetCopyAvailability(string copyId, bool available)
    {
        var content = new { Available = available };
        var response = await httpClient.PutAsJsonAsync($"setAvailability/{copyId}", content);
        return response.IsSuccessStatusCode;
    }
}
