using EventMangamentAPI.Service.Interface;
using EventMangamentAPI.ViewModel;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EventMangamentAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ParticipantsController : ControllerBase
    {
        private readonly IParticipantService _participantService;

        public ParticipantsController(IParticipantService participantService)
        {
            _participantService = participantService;
        }

        [HttpGet]
        public IActionResult GetParticipants()
        {
            var participants = _participantService.GetAllParticipants(out string errorMessage);
            if (participants == null)
            {
                return NotFound(errorMessage);
            }
            return Ok(participants);
        }

        [HttpGet("{id}")]
        public IActionResult GetParticipantById(int id)
        {
            var participantVm = _participantService.GetParticipantById(id, out string errorMessage);
            if (participantVm == null)
            {
                return NotFound(errorMessage);
            }
            return Ok(participantVm);
        }

        [HttpPost]
        public IActionResult CreateParticipant([FromBody] CreateParticipantVM request)
        {
            var result = _participantService.CreateParticipant(request, out string errorMessage);
            if (!result)
            {
                return BadRequest(errorMessage);
            }
            return CreatedAtAction(nameof(GetParticipantById), new { id = request.Id }, request);
        }

        [HttpPut("{id}")]
        public IActionResult UpdateParticipant(int id, [FromBody] UpdateParticipantVM request)
        {
            var result = _participantService.UpdateParticipant(id, request, out string errorMessage);
            if (!result)
            {
                return BadRequest(errorMessage);
            }
            return Ok();
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteParticipant(int id)
        {
            var result = _participantService.DeleteParticipant(id, out string errorMessage);
            if (!result)
            {
                return NotFound(errorMessage);
            }
            return NoContent();
        }
    }
}
