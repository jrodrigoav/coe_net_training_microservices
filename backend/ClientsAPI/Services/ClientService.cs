using ClientsAPI.Contracts;
using ClientsAPI.DTO;
using ClientsAPI.Persistency;

namespace ClientsAPI.Services
{
    public class ClientService : IClientService
    {
        private readonly ClientsDbContext _context;

        public ClientService(ClientsDbContext context)
        {
            _context = context;
        }

        public async Task<List<Client>> ListAsync() => await _context.GetAllAsync();

        public async Task<Client> CreateAsync(Client client)
        {
            client.Id = Guid.NewGuid().ToString();
            await _context.Add(client);           
            return client;
        }

        public Task<Client> UpdateAsync(Client client)
        {
            throw new NotImplementedException();
        }

        public async Task DeleteAsync(string id)
        {
            var client = await _context.GetClientByIdAsync(id);
            if (client is not null) await _context.DeleteClientAsync(client);
        }

        public Task<Client> GetAsync(string id) => _context.GetClientByIdAsync(id);
    }
}
