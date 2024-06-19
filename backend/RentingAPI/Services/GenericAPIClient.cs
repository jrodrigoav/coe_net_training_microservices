using Microsoft.Extensions.Options;
using RentingAPI.Models;

namespace RentingAPI.Services
{
    public class GenericAPIClient<T> where T : GenericSettings
    {
        protected readonly HttpClient _client;

        public GenericAPIClient(HttpClient client, IOptionsMonitor<T> optionsMonitor)
        {
            _client = client;
            _client.BaseAddress = optionsMonitor.CurrentValue.Url;
        }
    }
}
