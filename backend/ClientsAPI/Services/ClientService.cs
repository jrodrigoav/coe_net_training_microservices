using ClientsAPI.DTO;
using ClientsAPI.Services.Interface;

namespace ClientsAPI.Services
{
    public class ClientService : IClientService
    {
        public async Task<Client> CreateAsync(Client client)
        {
            return await Task.FromResult(new Client());
        }

        public async Task<Client> GetAsync(string id)
        {
            return await Task.FromResult(new Client());
        }

        public async Task<List<Client>> ListAsync()
        {
            return await Task.FromResult(new List<Client>());
        }

        public async Task<Client> UpdateAsync(string id, Client newClient)
        {
            return await Task.FromResult(new Client());
        }
    }
}
