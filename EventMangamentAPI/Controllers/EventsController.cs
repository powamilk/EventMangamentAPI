using EventMangamentAPI.Service.Interface;
using EventMangamentAPI.Service.Implement;
using EventMangamentAPI.ViewModel;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EventMangamentAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EventsController : ControllerBase
    {
        private readonly IEventService _eventService;

        public EventsController(IEventService eventService)
        {
            _eventService = eventService;
        }

        [HttpGet]
        public IActionResult GetEvents()
        {
            var events = _eventService.GetAllEvents(out string errorMessage);
            if (events == null)
            {
                return NotFound(errorMessage);
            }
            return Ok(events);
        }

        [HttpGet("{id}")]
        public IActionResult GetEventById(int id)
        {
            var eventVm = _eventService.GetEventById(id, out string errorMessage);
            if (eventVm == null)
            {
                return NotFound(errorMessage);
            }
            return Ok(eventVm);
        }

        [HttpPost]
        public IActionResult CreateEvent([FromBody] CreateEventVM request)
        {
            var result = _eventService.CreateEvent(request, out string errorMessage);
            if (!result)
            {
                return BadRequest(errorMessage);
            }
            return CreatedAtAction(nameof(GetEventById), new { id = request.Id }, request);
        }

        [HttpPut("{id}")]
        public IActionResult UpdateEvent(int id, [FromBody] UpdateEventVM request)
        {
            var result = _eventService.UpdateEvent(id, request, out string errorMessage);
            if (!result)
            {
                return BadRequest(errorMessage);
            }
            return Ok();
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteEvent(int id)
        {
            var result = _eventService.DeleteEvent(id, out string errorMessage);
            if (!result)
            {
                return NotFound(errorMessage);
            }
            return NoContent();
        }
    }
}
