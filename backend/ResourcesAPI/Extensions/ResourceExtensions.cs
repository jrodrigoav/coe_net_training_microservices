using ResourcesAPI.Models;
using ResourcesAPI.Models.Data;

namespace ResourcesAPI.Extensions
{
    public static class ResourceExtensions
    {
        public static Resource ToResource(this ResourceDTO resourceDTO)
        {
            return new Resource
            {
                Author = resourceDTO.Author,
                DateOfPublication = resourceDTO.DateOfPublication,
                Description = resourceDTO.Description,
                Name = resourceDTO.Name,
                Tags = resourceDTO.Tags,
                Type = resourceDTO.Type
            };
        }

        public static ResourceResponse ToResourceResponse(this Resource resource)
        {
            return new ResourceResponse
            {
                Author = resource.Author,
                DateOfPublication = resource.DateOfPublication,
                Description = resource.Description,
                Id = resource.Id,
                Name = resource.Name,
                Tags = resource.Tags,
                Type = resource.Type
            };
        }
    }
}
