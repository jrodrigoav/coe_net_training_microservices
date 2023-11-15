using ClientsAPI.DTO;

namespace ClientsAPI.Contracts
{
    public interface IClientService
    {
        Task<List<Client>> ListAsync();
        Task<Client> CreateAsync(Client client);
        Task<Client> UpdateAsync(Client client);
        Task DeleteAsync(string id);
        Task<Client> GetAsync(string id);
    }
}
