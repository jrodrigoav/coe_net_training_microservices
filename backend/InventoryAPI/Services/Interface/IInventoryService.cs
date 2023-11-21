using InventoryAPI.Models;

namespace InventoryAPI.Services.Interface
{
    public interface IInventoryService
    {
        public Task<List<InventoryModel>> ListAsync(RequestModel request);
        public Task<ResponseModel> SummaryAsync();
        public Task<InventoryModel> RegisterAsync(InventoryModel model);
        public Task<InventoryModel> SetAvailabilityAsync(RequestModel request);
    }
}
