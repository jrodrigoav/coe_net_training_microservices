using RentingAPI.Models;

namespace RentingAPI.Services.Interface
{
    public interface IRentingService
    {
        public Task<ResponseModel> List();
        public Task<ResponseModel> ListById(string id);
        public Task<RentingModel> Register(RentingModel model);
        public Task<string> Return(string id);
    }
}
