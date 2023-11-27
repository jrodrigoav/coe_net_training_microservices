using RentingAPI.Models;

namespace RentingAPI.Repository.Interface
{
    public interface IDynamoRepository
    {
        public Task<ResponseModel> List();
        public Task<List<ResponseModel>> GetListFiltered(FiltersModel filterModels);
        public Task<string> GetResourceById(string id);
        public Task<RentingModel> Register(RentingModel model);
        public Task<RentingModel> GetItem(string id);
        public Task<RentingModel> SetAvailability(string id, bool available);
    }
}
