using agendamento_recursos.DTOs.Resource;
using agendamento_recursos.Repository.Resource;
using Microsoft.AspNetCore.Mvc;

namespace agendamento_recursos.Controllers
{
    [ApiController]
    [Route("api/resource")]
    public class ResourceController : ControllerBase
    {
        private readonly IResourceRepository resourceRepository;

        public ResourceController(IResourceRepository resourceRepository)
        {
            this.resourceRepository = resourceRepository;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<ResourceDto>>> GetAll()
        {
            var resources = await resourceRepository.GetAllAsync();
            Console.WriteLine(resources);
            var dtos = resources.Select(r => new ResourceDto
            {
                Id = r.Id,
                Name = r.Name,
                IntervalMinutes = r.IntervalMinutes
            });
            return Ok(dtos);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ResourceDto>> GetById(int id)
        {
            var resource = await resourceRepository.GetByIdAsync(id);
            if (resource == null)
                return NotFound(new { message = "Recurso não encontrado" });

            var dto = new ResourceDto
            {
                Id = resource.Id,
                Name = resource.Name,
                IntervalMinutes = resource.IntervalMinutes
            };
            return Ok(dto);
        }
    }
}