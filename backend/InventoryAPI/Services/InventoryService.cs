using InventoryAPI.Contracts;
using InventoryAPI.DTO;
using InventoryAPI.Persistency;

namespace InventoryAPI.Services
{
    public class InventoryService : IInventoryService
    {
        private readonly IHttpRespondeReader _httpResponseReader;
        private readonly InventoryDbContext _context;

        public InventoryService(IHttpRespondeReader httpResponseReader, InventoryDbContext context)
        {
            _httpResponseReader = httpResponseReader;
            _context = context;
        }

        public async Task<List<Inventory>> GetAllAsync() => await _context.GetAllAsync();

        public async Task<Inventory> GetInventoryByIDAsync(Guid id) => await _context.GetByIDAsync(id);

        public async Task<Inventory> RegisterInventoryAsync(Inventory inventory)
        {
            var response = await _httpResponseReader.ReadResponseAsync("");
            inventory.Available = true;
            inventory.Data = response;
            await _context.RegisterAsync(inventory);
            return inventory;
        }

        public async Task<Inventory> UpdateAsync(Guid id, Inventory inventory) => await _context.UpdateAsync(id, inventory);
    }
}
