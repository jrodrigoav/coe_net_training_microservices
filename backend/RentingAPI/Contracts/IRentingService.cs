using RentingAPI.DTO;

namespace RentingAPI.Contracts
{
    public interface IRentingService
    {
        Task<List<Renting>> GetAllAsync();
        Task<Renting> GetByIDAsync(Guid id);
        Task<Renting> RegisterAsync(Renting renting);
        Task<List<Renting>> GetByClientIDAsync(Guid id);
        Task <Renting> ReturnAsync(Guid rentingID);
        Task <InventoryDTO> SetCopyAvailabilityAsync(Guid copyID);
    }
}
