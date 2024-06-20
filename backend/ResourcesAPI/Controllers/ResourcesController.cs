using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RentingAPI.Services.Data;
using ResourcesAPI.Extensions;
using ResourcesAPI.Models;
using ResourcesAPI.Models.Data;
using ResourcesAPI.Services.Data;
using System.Collections.Generic;
using System.Diagnostics;


namespace ResourcesAPI.Controllers
{

    [Route("api/resources"), ApiController]
    public class ResourcesController(ResourcesDbContext resourcesDbContext) : ControllerBase
    {
        [HttpGet("list")]
        public ActionResult<ResourceResponse[]> List()
        {
            var resources = resourcesDbContext.Resources.AsNoTracking();
            return Ok(resources.Select(r => r.ToResourceResponse()).ToArray());
        }

        [HttpGet("{resourceId}")]
        public async Task<ActionResult<Resource>> GetResourceById(Guid resourceId)
        {
            var r = await resourcesDbContext.Resources.FindAsync(resourceId);
            if (r == null) return NotFound();
            var data = r.ToResourceResponse();
            return Ok(data);
        }


        [HttpPost("create"), HttpOptions]
        public async Task<ActionResult<ResourceResponse>> Create(CreateResourceRequest createResourceRequest)
        {
            var entity = resourcesDbContext.Add(createResourceRequest.ToResource());
            var rows = await resourcesDbContext.SaveChangesAsync();
            if (rows != 1) return BadRequest();
            return CreatedAtAction(nameof(GetResourceById), new { resourceId = entity.Entity.Id }, entity.Entity.ToResourceResponse());
        }


        [HttpPut("update"), HttpOptions]
        public async Task<ActionResult<ResourceResponse>> Update(UpdateResourceRequest updateResourceRequest)
        {
            var r = await resourcesDbContext.Resources.FindAsync(updateResourceRequest.Id);
            if (r == null) return NotFound();
            r.Author = updateResourceRequest.Author;
            r.Description = updateResourceRequest.Description;
            r.DateOfPublication = updateResourceRequest.DateOfPublication;
            r.Name = updateResourceRequest.Name;
            r.Tags = updateResourceRequest.Tags;
            r.Type = updateResourceRequest.Type;
            await resourcesDbContext.SaveChangesAsync();
            return Ok(r.ToResourceResponse());

        }

        [HttpDelete("{resourceId}"), ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<ActionResult> Delete(Guid resourceId)
        {
            var resource = await resourcesDbContext.Resources.FindAsync(resourceId);
            if (resource == null) return NotFound();
            resourcesDbContext.Remove(resource);
            await resourcesDbContext.SaveChangesAsync();
            return NoContent();
        }
    }
}