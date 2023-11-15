using ClientsAPI.DTO;

namespace ClientsAPI.Persistency
{
    //Mocked dbcontext operations.
    public class ClientsDbContext
    {
        private List<Client> Clients { get; set; } = new List<Client>();

        public async Task Add(Client client) => await Task.Run(() => Clients.Add(client));
        public async Task<Client> GetClientByIdAsync(string id) => await Task.Run(() => Clients.FirstOrDefault(c => c.Id == id)!);
        public async Task DeleteClientAsync(Client client) => await Task.Run(() => Clients.Remove(client));
        public async Task<List<Client>> GetAllAsync() => await Task.Run(() => Clients);
    }
}
