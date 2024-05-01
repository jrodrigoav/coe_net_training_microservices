using InventoryAPI.Models;
using InventoryAPI.Models.Data;
using InventoryAPI.Services.Data;
using Microsoft.EntityFrameworkCore;

namespace InventoryAPI.Services
{
    public class InventoryService
    {
        private readonly InventoryDbContext _inventoryDbContext;
        private readonly ResourceAPIClient _resourceAPIClient;

        public InventoryService(InventoryDbContext inventoryDbContext, ResourceAPIClient resourceAPIClient)
        {
            _inventoryDbContext = inventoryDbContext;
            _resourceAPIClient = resourceAPIClient;
        }

        public async Task<Item?> RegisterAsync(Item item)
        {
            var resource = await _resourceAPIClient.GetByResourceIdAsync(item.ResourceId);
            if (resource == null) return null;
            var entity = _inventoryDbContext.Items.Add(item);
            await _inventoryDbContext.SaveChangesAsync();
            return entity.Entity;
        }

        public Item[] ListResourceAvailability(Guid resourceId, bool available)
        {
            var items = _inventoryDbContext.Items.Where(i => i.ResourceId == resourceId && i.Available == available).AsNoTracking().ToArray();
            return items ?? [];
        }

        public async Task<Item?> UpdateItemAvailabilityAsync(Guid itemId, bool available)
        {
            var item = _inventoryDbContext.Items.Find(itemId);
            if (item == null) return null;
            item.Available = available;
            await _inventoryDbContext.SaveChangesAsync();
            return item;
        }

        public async Task<Summary[]> GetSummaryAsync()
        {
            var resources = await _resourceAPIClient.ListResourcesAsync();
            var result = from items in _inventoryDbContext.Items
                         join res in resources on items.ResourceId equals res.Id
                         group items by new { items.ResourceId, res.Name } into g
                         select new Summary
                         {
                             ResourceId = g.Key.ResourceId,
                             ResourceName = g.Key.Name,
                             AvailableCopies = g.Count(x => x.Available),
                             UnavailableCopies = g.Count(x => !x.Available),
                             TotalCopies = g.Count()
                         };
            return [.. result];
        }
    }
}