
namespace InventoryAPI.Services
{
    public interface IInventoryService
    {
        public Task<IAsyncResult> RegisterAsync(string id);
        public Task<IAsyncResult> ListAsync(string id);
        public Task<IAsyncResult> SetAvailabilityAsync(string id);
        public Task<IAsyncResult> SummaryAsync(string id);
    }
}
