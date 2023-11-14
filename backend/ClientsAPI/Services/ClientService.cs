using ClientsAPI.DTO;

namespace ClientsAPI.Services
{
    public class ClientService
    {
        public async Task<List<Client>> ListAsync()
        {
            return await Task.FromResult(new List<Client>());
        }
    }
}
