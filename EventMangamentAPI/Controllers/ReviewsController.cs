using EventMangamentAPI.Service.Interface;
using EventMangamentAPI.ViewModel;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EventMangamentAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReviewsController : ControllerBase
    {
        private readonly IReviewService _reviewService;

        public ReviewsController(IReviewService reviewService)
        {
            _reviewService = reviewService;
        }

        [HttpGet]
        public IActionResult GetReviews()
        {
            var reviews = _reviewService.GetAllReviews(out string errorMessage);
            if (reviews == null)
            {
                return NotFound(errorMessage);
            }
            return Ok(reviews);
        }

        [HttpGet("{id}")]
        public IActionResult GetReviewById(int id)
        {
            var reviewVm = _reviewService.GetReviewById(id, out string errorMessage);
            if (reviewVm == null)
            {
                return NotFound(errorMessage);
            }
            return Ok(reviewVm);
        }

        [HttpPost]
        public IActionResult CreateReview([FromBody] CreateReviewVM request)
        {
            var result = _reviewService.CreateReview(request, out string errorMessage);
            if (!result)
            {
                return BadRequest(errorMessage);
            }
            return CreatedAtAction(nameof(GetReviewById), new { id = request.Id }, request);
        }

        [HttpPut("{id}")]
        public IActionResult UpdateReview(int id, [FromBody] UpdateReviewVM request)
        {
            var result = _reviewService.UpdateReview(id, request, out string errorMessage);
            if (!result)
            {
                return BadRequest(errorMessage);
            }
            return Ok();
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteReview(int id)
        {
            var result = _reviewService.DeleteReview(id, out string errorMessage);
            if (!result)
            {
                return NotFound(errorMessage);
            }
            return NoContent();
        }
    }
}
