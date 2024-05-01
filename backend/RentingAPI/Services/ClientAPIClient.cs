using Microsoft.Extensions.Options;
using RentingAPI.Models;

namespace RentingAPI.Services
{
    public class ClientAPIClient : GenericAPIClient<ClientsAPISettings>
    {
        public ClientAPIClient(HttpClient client, IOptionsMonitor<ClientsAPISettings> optionsMonitor) : base(client, optionsMonitor)
        {

        }
    }


}
