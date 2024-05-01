using Microsoft.Extensions.Options;
using RentingAPI.Models;
using RentingAPI.Models.ResourcesAPI;

namespace RentingAPI.Services
{
    public class ResourcesAPIClient : GenericAPIClient<ResourcesAPISettings>
    {
        public ResourcesAPIClient(HttpClient client, IOptionsMonitor<ResourcesAPISettings> optionsMonitor) : base(client, optionsMonitor)
        {

        }

        public async Task<ResourceResponse?> GetResourceByIdAsync(Guid resourceId)
        {
            var resource = await _client.GetFromJsonAsync<ResourceResponse>($"{resourceId}");
            return resource;
        }
    }


}
