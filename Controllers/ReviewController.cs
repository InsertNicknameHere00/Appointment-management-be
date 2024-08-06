using AppointmentAPI.Entities;
using AppointmentAPI.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace AppointmentAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReviewController : ControllerBase
    {
        private readonly IReviewService reviewService;

        public ReviewController(IReviewService _reviewService)
        {
            reviewService = _reviewService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Review>>> GetAllReviews()
        {
            var reviews = await reviewService.GetAllReviews();
            return Ok(reviews);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Review>> GetReviewById([FromHeader]int id)
        {
            var review = await reviewService.GetReviewById(id);
            if (review == null)
            {
                return NotFound();
            }
            return Ok(review);
        }

        [HttpGet("service/{serviceId}")]
        public async Task<ActionResult<IEnumerable<Review>>> GetReviewsByServiceId([FromHeader]int serviceId)
        {
            var reviews = await reviewService.GetReviewByServiceId(serviceId);
            return Ok(reviews);
        }

        [HttpGet("user/{userId}")]
        public async Task<ActionResult<IEnumerable<Review>>> GetReviewsByUserId([FromHeader] int userId)
        {
            var reviews = await reviewService.GetReviewByUserId(userId);
            return Ok(reviews);
        }

        [HttpPost]
       // [Authorize]
        public async Task<ActionResult<Review>> AddReview([FromBody] Review _review)
        {
            var review = await reviewService.Save(_review);
            return Ok(review);
        }

        [HttpPut("{id}")]
        //[Authorize]
        public async Task<ActionResult> UpdateReview([FromHeader]int id, [FromBody] Review _review)
        {
            var review = await reviewService.Update(id, _review);
            return Ok(review);
        }

        [HttpDelete("{id}")]
        //[Authorize]
        public async Task<bool> DeleteReview([FromHeader]int id)
        {
            var deleted = await reviewService.Delete(id);
            if (!deleted)
            {
                return false;
            }

            return true;
        }


    }
}
