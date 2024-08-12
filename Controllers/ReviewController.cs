using AppointmentAPI.Entities;
using AppointmentAPI.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.DotNet.Scaffolding.Shared.Messaging;
using System.Security.Claims;

namespace AppointmentAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReviewController : ControllerBase
    {
        private readonly IReviewService reviewService;
        private readonly ILogger<ReviewController> logger;

        public ReviewController(IReviewService _reviewService, ILogger<ReviewController> logger)
        {
            reviewService = _reviewService;
            this.logger = logger;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Review>>> GetAllReviews()
        {
            try
            {
                var reviews = await reviewService.GetAllReviews();
                logger.LogInformation("Reviews are successfully loaded");
                return Ok(reviews);
            }
            catch(KeyNotFoundException ex)
            {
                logger.LogInformation("Reviews does not exist");
                return NotFound(new {message = ex.Message});
            }
            catch (Exception ex) 
            { 
                logger.LogInformation("Server error");
                return StatusCode(StatusCodes.Status500InternalServerError, new { message = "We are currently unable to process your request!" });
            }
            
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Review>> GetReviewById([FromHeader]int id)
        {
            try
            {
                var review = await reviewService.GetReviewById(id);
                logger.LogInformation("Review is successfully loaded");
                return Ok(review);
            }
            catch (KeyNotFoundException ex)
            {
                logger.LogInformation("Review with this id does not exist");
                return NotFound(new { message = ex.Message });
            }
            catch (Exception ex)
            {
                logger.LogInformation("Server error");
                return StatusCode(StatusCodes.Status500InternalServerError, new { message = "We are currently unable to process your request!" });
            }

        }

        [HttpGet("service/{serviceId}")]
        public async Task<ActionResult<IEnumerable<Review>>> GetReviewsByServiceId([FromHeader]int serviceId)
        {
            try
            {
                var reviews = await reviewService.GetReviewByServiceId(serviceId);
                logger.LogInformation("Reviews are successfully loaded");
                return Ok(reviews);
            }
            catch (KeyNotFoundException ex)
            {
                logger.LogInformation("Reviews for this service does not exist");
                return NotFound(new { message = ex.Message });
            }
            catch (Exception ex)
            {
                logger.LogInformation("Server error");
                return StatusCode(StatusCodes.Status500InternalServerError, new { message = "We are currently unable to process your request!" });
            }
        }

        [HttpGet("user/{userId}")]
        public async Task<ActionResult<IEnumerable<Review>>> GetReviewsByUserId([FromHeader] int userId)
        {
            try
            {
                var reviews = await reviewService.GetReviewByUserId(userId);
                logger.LogInformation("Reviews are successfully loaded");
                return Ok(reviews);
            }
            catch (KeyNotFoundException ex)
            {
                logger.LogInformation("Reviews by this user does not exist");
                return NotFound(new { message = ex.Message });
            }
            catch (Exception ex)
            {
                logger.LogInformation("Server error");
                return StatusCode(StatusCodes.Status500InternalServerError, new { message = "We are currently unable to process your request!" });
            }
        }

        [HttpPost]
        [Authorize(Roles = "User")]
        public async Task<ActionResult<Review>> AddReview([FromBody] Review _review)
        {
            try
            {
                var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
                if (string.IsNullOrEmpty(userId))
                {
                    return Unauthorized(new { message = "User ID not found in the token." });
                }

                _review.UserId = int.Parse(userId);


                var review = await reviewService.Save(_review);
                logger.LogInformation("Review is successfully created");
                return Ok(review);
            }
            catch (KeyNotFoundException ex)
            {
                logger.LogInformation("Review is not created successfully");
                return NotFound(new { message = ex.Message });
            }
            catch (Exception ex)
            {
                logger.LogInformation("Server error");
                return StatusCode(StatusCodes.Status500InternalServerError, new { message = "We are currently unable to process your request!" });
            }
        }

        [HttpPut("{id}")]
        [Authorize]
        public async Task<ActionResult> UpdateReview([FromHeader]int id, [FromBody] Review _review)
        {
            try
            {
                var review = await reviewService.Update(id, _review);
                logger.LogInformation("The review is successfully updated");
                return Ok(review);
            }
            catch (KeyNotFoundException ex)
            {
                logger.LogInformation("Review is not found");
                return NotFound(new { message = ex.Message });
            }
            catch (Exception ex)
            {
                logger.LogInformation("Server error");
                return StatusCode(StatusCodes.Status500InternalServerError, new { message = "We are currently unable to process your request!" });
            }
        }

        [HttpDelete("{id}")]
        [Authorize]
        public async Task<IActionResult> DeleteReview([FromHeader]int id)
        {
            try
            {
                var reviews = await reviewService.Delete(id);
                logger.LogInformation("Review is deleted successfully");
                return Ok(reviews);
            }
            catch (KeyNotFoundException ex)
            {
                logger.LogInformation("Review is not found");
                return NotFound(new { message = ex.Message });
            }
            catch (Exception ex)
            {
                logger.LogInformation("Server error");
                return StatusCode(StatusCodes.Status500InternalServerError, new { message = "We are currently unable to process your request!" });
            }


        }


    }
}
