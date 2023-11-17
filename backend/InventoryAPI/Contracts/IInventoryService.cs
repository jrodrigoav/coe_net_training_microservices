using InventoryAPI.DTO;

namespace InventoryAPI.Contracts
{
    public interface IInventoryService
    {
        Task<Inventory> RegisterInventoryAsync(Inventory inventory);
        Task<Inventory> GetInventoryByIDAsync(Guid id);
        Task<List<Inventory>> GetAllAsync();

        Task<Inventory> UpdateAsync(Guid id, Inventory inventory);
    }
}
