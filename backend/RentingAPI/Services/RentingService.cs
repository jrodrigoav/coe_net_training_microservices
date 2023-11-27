using RentingAPI.Contracts;
using RentingAPI.DTO;
using RentingAPI.Persistency;
using System.Net;
using System.Text;
using System.Text.Json;

namespace RentingAPI.Services
{
    public class RentingService : IRentingService
    {
        private readonly RentingDbContext _rentingDbContext;
        private readonly IHttpClientFactory _clientFactory;

        public RentingService(RentingDbContext rentingDbContext, IHttpClientFactory clientFactory)
        {
            _rentingDbContext = rentingDbContext;
            _clientFactory = clientFactory;
        }

        public Task<List<Renting>> GetAllAsync() => _rentingDbContext.GetAllAsync();

        public Task<List<Renting>> GetByClientIDAsync(Guid id) => _rentingDbContext.GetByClientIDAsync(id);

        public Task<Renting> GetByIDAsync(Guid id) => _rentingDbContext.GetByIDAsync(id);

        public Task<Renting> RegisterAsync(Renting renting) => _rentingDbContext.RegisterAsync(renting);

        public Task<Renting> ReturnAsync(Guid rentingID) => _rentingDbContext.ReturnAsync(rentingID);

        public async Task<InventoryDTO> SetCopyAvailabilityAsync(Guid copyID)
        {
            var response = await _clientFactory.CreateClient("Inventory").PutAsync($"api/inventory/{copyID}", new StringContent(
                "{\"Available\": true}",
                Encoding.UTF8,
                "application/json"
            ));

            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsByteArrayAsync();
                var options = new JsonSerializerOptions() { PropertyNameCaseInsensitive = true };
                var result = JsonSerializer.Deserialize<InventoryDTO>(content, options);
                return result!;
            }

            return null!;
        }
    }
}
