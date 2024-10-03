using EventMangamentAPI.Service.Interface;
using EventMangamentAPI.ViewModel;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EventMangamentAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrtherController : ControllerBase
    {
        private readonly IOrtherService _ortherService;

        public OrtherController(IOrtherService ortherService)
        {
            _ortherService = ortherService;
        }

        // GET /api/events/search
        [HttpGet("events/search")]
        public IActionResult SearchEvents([FromQuery] string name, [FromQuery] string location, [FromQuery] DateTime? startDate, [FromQuery] DateTime? endDate)
        {
            var events = _ortherService.SearchEvents(name, location, startDate, endDate);
            return Ok(new { events });
        }

        // GET /api/events/filter
        [HttpGet("events/filter")]
        public IActionResult FilterEvents([FromQuery] string status)
        {
            var events = _ortherService.FilterEventsByStatus(status);
            return Ok(new { events });
        }

        // GET /api/events/stats
        [HttpGet("events/stats")]
        public IActionResult GetEventStatistics([FromQuery] DateTime? startDate, [FromQuery] DateTime? endDate, [FromQuery] string status)
        {
            var stats = _ortherService.GetEventStatistics(startDate, endDate, status);
            return Ok(stats);
        }

        // POST /api/registrations/bulk
        [HttpPost("registrations/bulk")]
        public IActionResult BulkRegisterParticipants([FromBody] BulkRegistrationVM request)
        {
            var registrations = _ortherService.BulkRegisterParticipants(request);
            return Ok(new { message = $"Đăng ký thành công cho {registrations.Count} người tham gia.", registrations });
        }

        // PUT /api/registrations/{id}/status
        [HttpPut("registrations/{id}/status")]
        public IActionResult UpdateRegistrationStatus(int id, [FromBody] UpdateRegistrationStatusVM request)
        {
            var registration = _ortherService.UpdateRegistrationStatus(id, request.Status);
            return Ok(new { message = "Cập nhật trạng thái đăng ký thành công.", registration });
        }

        // POST /api/reviews/from-organizers
        [HttpPost("reviews/from-organizers")]
        public IActionResult CreateOrganizerReview([FromBody] CreateReviewVM request)
        {
            var review = _ortherService.CreateOrganizerReview(request);
            return Ok(new { message = "Đánh giá thành công.", review });
        }
    }
}
