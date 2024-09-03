using EventMangamentAPI.Service.Interface;
using EventMangamentAPI.ViewModel;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EventMangamentAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrganizersController : ControllerBase
    {
        private readonly IOrganizerService _organizerService;

        public OrganizersController(IOrganizerService organizerService)
        {
            _organizerService = organizerService;
        }

        [HttpGet]
        public IActionResult GetOrganizers()
        {
            var organizers = _organizerService.GetAllOrganizers(out string errorMessage);
            if (organizers == null)
            {
                return NotFound(errorMessage);
            }
            return Ok(organizers);
        }

        [HttpGet("{id}")]
        public IActionResult GetOrganizerById(int id)
        {
            var organizerVm = _organizerService.GetOrganizerById(id, out string errorMessage);
            if (organizerVm == null)
            {
                return NotFound(errorMessage);
            }
            return Ok(organizerVm);
        }

        [HttpPost]
        public IActionResult CreateOrganizer([FromBody] CreateOrganizerVM request)
        {
            var result = _organizerService.CreateOrganizer(request, out string errorMessage);
            if (!result)
            {
                return BadRequest(errorMessage);
            }
            return CreatedAtAction(nameof(GetOrganizerById), new { id = request.Id }, request);
        }

        [HttpPut("{id}")]
        public IActionResult UpdateOrganizer(int id, [FromBody] UpdateOrganizerVM request)
        {
            var result = _organizerService.UpdateOrganizer(id, request, out string errorMessage);
            if (!result)
            {
                return BadRequest(errorMessage);
            }
            return Ok();
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteOrganizer(int id)
        {
            var result = _organizerService.DeleteOrganizer(id, out string errorMessage);
            if (!result)
            {
                return NotFound(errorMessage);
            }
            return NoContent();
        }
    }
}
