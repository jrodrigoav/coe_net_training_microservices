using InventoryAPI.Models;
using InventoryAPI.Services.Interface;

namespace InventoryAPI.Services
{
    public class InventoryService : IInventoryService
    {
        public async Task<ResponseModel> SummaryAsync()
        {
            try
            {
                ResponseModel responseModel = new ResponseModel();
                // get the list
                var data = GetSumaryList();
                responseModel.AvailableCopies = data.Result.Count(); // Get available
                responseModel.UnavailableCopies = data.Result.Count(); // Get unavailable
                responseModel.TotalCopies = (data.Result.Count() + data.Result.Count()); // Get the result of available and unavailable 
                return await Task.FromResult(responseModel);
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public async Task<List<InventoryModel>> ListAsync(RequestModel request)
        {
            try
            {
                string id = request.Id;
                bool isAvailable = request.IsAvailable; /// To filter only Available items
                return await Task.FromResult(new List<InventoryModel>());
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public async Task<InventoryModel> RegisterAsync(InventoryModel model)
        {
            try
            {
                // create the registry
                return await Task.FromResult(new InventoryModel());
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public async Task<InventoryModel> SetAvailabilityAsync(RequestModel request)
        {
            try
            {
                bool available = request.IsAvailable; // property to set the availability
                return await Task.FromResult(new InventoryModel());
            }
            catch (Exception ex)
            {
                throw;
            }            
        }

        private async Task<List<int>> GetSumaryList()
        {
            return await Task.FromResult(new List<int>());
        }

    }
}
