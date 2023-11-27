using RentingAPI.DTO;

namespace RentingAPI.Persistency
{
    public class RentingDbContext
    {
        private List<Renting> Rentings { get; set; } = new List<Renting>();
        public async Task<List<Renting>> GetAllAsync() => await Task.Run(() => Rentings);   
        public async Task<List<Renting>> GetByClientIDAsync(Guid clientId) => await Task.Run(() => Rentings.Where(x => x.ClientID == clientId).ToList()!);

        public async Task<Renting> GetByIDAsync(Guid id) => await Task.Run(() => Rentings.FirstOrDefault(x => x.ID == id)!);

        public Task<Renting> RegisterAsync(Renting renting)
        {
            renting.ID = Guid.NewGuid();
            Rentings.Add(renting);
            return Task.Run(() => renting);
        }

        internal Task<Renting> ReturnAsync(Guid rentingID)
        {
            var renting = Rentings.FirstOrDefault(x => x.ID == rentingID);
            renting!.ReturnDate = DateTime.Now;
            return Task.Run(() => renting); 
        }
    }
}
