using ResourcesAPI.Models;
using ResourcesAPI.Services.Interface;

namespace ResourcesAPI.Services
{
    public class ResourcesService : IResourcesService
    {
        public Task<ResourceModel> CreateAsync(ResourceModel Model)
        {
            try
            {
                var result = new ResourceModel();
                return Task.FromResult(result);
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public void DeleteAsync(string id)
        {
            try
            {
                var removed = id;
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public Task<ResourceModel> GetAsync(string id)
        {
            try
            {
                var result = new ResourceModel();
                return Task.FromResult(result);
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public Task<List<ResourceModel>> ListAsync()
        {
            try
            {
                var list = new List<ResourceModel>();
                return Task.FromResult(list);
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public Task<ResourceModel> UpdateAsync(ResourceModel model)
        {
            try
            {
                var result = new ResourceModel();
                return Task.FromResult(result);
            }
            catch (Exception ex)
            {
                throw;
            }
        }
    }
}
