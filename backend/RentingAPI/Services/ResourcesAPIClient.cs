using Microsoft.Extensions.Options;
using RentingAPI.Models;
using RentingAPI.Models.ResourcesAPI;

namespace RentingAPI.Services
{
    public class ResourcesAPIClient : GenericAPIClient<ResourcesAPISettings>
    {
        public ResourcesAPIClient(HttpClient client, IOptionsMonitor<ResourcesAPISettings> optionsMonitor) : base(client, optionsMonitor)
        {
            _client.BaseAddress = optionsMonitor.CurrentValue.Url;
        }

        public async Task<ResourceResponse?> GetResourceByIdAsync(Guid resourceId)
        {
            var resource = await _client.GetFromJsonAsync<ResourceResponse>($"{_client.BaseAddress}/{resourceId}");
            return resource;
        }
    }


}
