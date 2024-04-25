using InventoryAPI.Models;

namespace Core.Services;

public class ResourceApi(HttpClient httpClient)
{
    public async Task<IEnumerable<Resource>?> GetAll()
    {
        return await httpClient.GetFromJsonAsync<IEnumerable<Resource>>("");
    }

    public async Task<Resource?> Get(string id)
    {
        return await httpClient.GetFromJsonAsync<Resource>(id);
    }
}
