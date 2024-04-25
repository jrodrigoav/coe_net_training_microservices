using ClientsAPI.Models;
using ClientsAPI.Models.Data;

namespace ClientsAPI.Extensions
{
    public static class ClientExtensions
    {
        public static Client ToClient(this CreateClientRequest request)
        {
            return new Client
            {
                Email = request.Email,
                FirstName = request.FirstName,
                LastName = request.LastName
            };
        }

        public static ClientResponse ToClientResponse(this Client client)
        {
            return new ClientResponse
            {
                Id = client.Id,
                FirstName = client.FirstName,
                LastName = client.LastName
            };
        }
    }
}
