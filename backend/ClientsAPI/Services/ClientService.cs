using ClientsAPI.DTO;

namespace ClientsAPI.Services
{
    public class ClientService : IClientService
    {
        public async Task<Client> CreateAsync(Client client)
        {
            client.Id = Guid.NewGuid().ToString();
            return await Task.FromResult(new Client());
        }
        public async Task<List<Client>> ListAsync()
        {
            return await Task.FromResult(new List<Client>());
        }

        public async Task<Client> GetAsync(string id) => await Task.FromResult(new Client());

        public async Task UpdateAsync(Client newClientData) => await Task.FromResult(new Client());
    }
}
