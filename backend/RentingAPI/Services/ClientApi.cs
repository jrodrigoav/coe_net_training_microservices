using RentingAPI.Models;

namespace RentingAPI.Services;

public class ClientApi(HttpClient httpClient)
{
    public async Task<Client?> Get(string id)
    {
        return await httpClient.GetFromJsonAsync<Client>(id);
    }
}
