using Microsoft.Extensions.Options;
using RentingAPI.Models;

namespace RentingAPI.Services
{
    public class ResourcesAPIClient : GenericAPIClient<ResourcesAPISettings>
    {
        public ResourcesAPIClient(HttpClient client, IOptionsMonitor<ResourcesAPISettings> optionsMonitor) : base(client, optionsMonitor)
        {

        }
    }


}
