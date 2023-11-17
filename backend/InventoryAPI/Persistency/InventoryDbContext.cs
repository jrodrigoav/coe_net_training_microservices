using InventoryAPI.DTO;

namespace InventoryAPI.Persistency
{
    //Mocked dbcontext operations.
    public class InventoryDbContext
    {
        private List<Inventory> Inventory { get; set; } = new List<Inventory>();

        public async Task RegisterAsync(Inventory inventory) => await Task.Run(() => Inventory.Add(inventory));
        public async Task<Inventory> GetByIDAsync(Guid id) => await Task.Run(() => Inventory.FirstOrDefault(x => x.ID == id)!);
        public async Task<List<Inventory>> GetAllAsync() => await Task.Run(() => Inventory);
        public async Task<Inventory> UpdateAsync(Guid id, Inventory inventory)
        {
            var existingInventory = await GetByIDAsync(id);
            existingInventory = inventory;
            return existingInventory;
        }

    }
}
