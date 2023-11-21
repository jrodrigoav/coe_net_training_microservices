using ClientsAPI.DTO;
using ClientsAPI.Services.Interface;

namespace ClientsAPI.Services
{
    public class ClientService : IClientService
    {
        public async Task<Client> CreateAsync(Client client)
        {
            try
            {
                return await Task.FromResult(new Client());

            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public async Task<Client> GetAsync(string id)
        {
            try
            {
                return await Task.FromResult(new Client());
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public async Task<List<Client>> ListAsync()
        {
            try
            {
                return await Task.FromResult(new List<Client>());
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public async Task<Client> UpdateAsync(string id, Client newClient)
        {
            try
            {
                return await Task.FromResult(new Client());
            }
            catch (Exception ex)
            {
                throw;
            }
        }
    }
}
