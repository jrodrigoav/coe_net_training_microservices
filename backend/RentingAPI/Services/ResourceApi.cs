using RentingAPI.Models;

namespace RentingAPI.Services;

public class ResourceApi(HttpClient httpClient)
{
    public async Task<Resource> Get(string id)
    {
        return await httpClient.GetFromJsonAsync<Resource>(id) ?? throw new NullReferenceException($"Couldn't retrieve resource by id {id}");
    }
}