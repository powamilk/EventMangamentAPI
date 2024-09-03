using EventMangamentAPI.Service.Interface;
using EventMangamentAPI.ViewModel;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EventMangamentAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RegistrationsController : ControllerBase
    {
        private readonly IRegistrationService _registrationService;

        public RegistrationsController(IRegistrationService registrationService)
        {
            _registrationService = registrationService;
        }

        [HttpGet]
        public IActionResult GetRegistrations()
        {
            var registrations = _registrationService.GetAllRegistrations(out string errorMessage);
            if (registrations == null)
            {
                return NotFound(errorMessage);
            }
            return Ok(registrations);
        }

        [HttpGet("{id}")]
        public IActionResult GetRegistrationById(int id)
        {
            var registrationVm = _registrationService.GetRegistrationById(id, out string errorMessage);
            if (registrationVm == null)
            {
                return NotFound(errorMessage);
            }
            return Ok(registrationVm);
        }

        [HttpPost]
        public IActionResult CreateRegistration([FromBody] CreateRegistrationVM request)
        {
            var result = _registrationService.CreateRegistration(request, out string errorMessage);
            if (!result)
            {
                return BadRequest(errorMessage);
            }
            return CreatedAtAction(nameof(GetRegistrationById), new { id = request.Id }, request);
        }

        [HttpPut("{id}")]
        public IActionResult UpdateRegistration(int id, [FromBody] UpdateRegistrationVM request)
        {
            var result = _registrationService.UpdateRegistration(id, request, out string errorMessage);
            if (!result)
            {
                return BadRequest(errorMessage);
            }
            return Ok();
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteRegistration(int id)
        {
            var result = _registrationService.DeleteRegistration(id, out string errorMessage);
            if (!result)
            {
                return NotFound(errorMessage);
            }
            return NoContent();
        }
    }
}
