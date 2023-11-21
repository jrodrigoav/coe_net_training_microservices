using ClientsAPI.DTO;
using Microsoft.AspNetCore.Mvc;

namespace ClientsAPI.Services.Interface
{
    public interface IClientService
    {
        public Task<List<Client>> ListAsync();
        public Task<Client> GetAsync(string id);
        public Task<Client> CreateAsync(Client client);
        public Task<Client> UpdateAsync(string id, Client newClient);
    }
}
