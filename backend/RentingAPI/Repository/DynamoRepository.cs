using RentingAPI.Models;
using RentingAPI.Repository.Interface;

namespace RentingAPI.Repository
{
    public class DynamoRepository : IDynamoRepository
    {
        public Task<RentingModel> GetItem(string id)
        {
            return Task.FromResult(new RentingModel());
        }

        public Task<List<ResponseModel>> GetListFiltered(FiltersModel filterModels)
        {
            return Task.FromResult(new List<ResponseModel>());
        }

        public Task<string> GetResourceById(string id)
        {
            string result = string.Empty;
            return Task.FromResult(result);
        }

        public Task<ResponseModel> List()
        {
            return Task.FromResult(new ResponseModel());
        }

        public Task<RentingModel> Register(RentingModel model)
        {
            return Task.FromResult(new RentingModel());
        }

        public Task<RentingModel> SetAvailability(string id, bool available)
        {
            return Task.FromResult(new RentingModel());
        }
    }
}
