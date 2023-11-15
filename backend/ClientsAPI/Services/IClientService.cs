using ClientsAPI.DTO;

namespace ClientsAPI.Services
{
    public interface IClientService
    {
        public Task<Client> CreateAsync(Client client);
        public Task<List<Client>> ListAsync();

        public Task<Client> GetAsync(string id);

        public Task UpdateAsync(Client newClientData);
    }
}
