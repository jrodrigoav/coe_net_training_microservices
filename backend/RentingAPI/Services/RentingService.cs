using RentingAPI.Models;
using RentingAPI.Repository.Interface;
using RentingAPI.Services.Interface;

namespace RentingAPI.Services
{
    public class RentingService : IRentingService
    {
        private readonly IDynamoRepository _dynamoRepositoy;
        public RentingService(IDynamoRepository dynamoRepositoy)
        {
            _dynamoRepositoy = dynamoRepositoy;
        }
        public async Task<ResponseModel> List()
        {
            try
            {
                ResponseModel responseModel = new ResponseModel();
                var result = _dynamoRepositoy.List();
                responseModel.Items = result.Result.Items;
                responseModel.Count = result.Result.Count;
                return await Task.FromResult(responseModel); 

            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public async Task<ResponseModel> ListById(string id)
        {
            try
            {
                ResponseModel responseModel = new ResponseModel();
                var filter = new FiltersModel();
                filter.expression = true;
                filter.attributeNames = "clientId";
                filter.attributeValues = true;
                var result = _dynamoRepositoy.GetListFiltered(filter);
                foreach (var item in result.Result)
                {
                    foreach (var it in item.Items)
                    {
                        it.ResourceName = await GetResourceNameById(it.ResourceId);
                    }
                }
                return responseModel;
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public async Task<RentingModel> Register(RentingModel model)
        {
            try
            {
                var result = await _dynamoRepositoy.Register(model);
                var copy = SetCopyAvailability(model.CopyId, false);
                return result;
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public Task<string> Return(string id)
        {
            try
            {
                var copy = GetCopyIdByRentingId(id);
                var result = SetCopyAvailability(copy.CopyId, false);
                return Task.FromResult(result.Result.ReturnDate);
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        private RentingModel GetCopyIdByRentingId(string rentingId)
        {
            try
            {
                RentingModel copy = new RentingModel();
                var result = _dynamoRepositoy.GetItem(rentingId);
                copy.CopyId = rentingId;
                return copy;
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        private async Task<RentingModel> SetCopyAvailability(string copyId, bool available = false)
        {
            try
            {
                var retult = await _dynamoRepositoy.SetAvailability(copyId, available);
                return retult;
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        private async Task<string> GetResourceNameById(string resourceId)
        {
            try
            {
                string resourceName = string.Empty;
                var result = await _dynamoRepositoy.GetResourceById(resourceId);
                return resourceName;
            }
            catch (Exception ex)
            {
                throw;
            }
        }
    }
}
