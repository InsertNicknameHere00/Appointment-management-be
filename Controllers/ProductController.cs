using AppointmentAPI.Entities;
using AppointmentAPI.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AppointmentAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IProductService productService;
        private readonly ILogger<ProductController> logger;

        public ProductController(IProductService _service, ILogger<ProductController> logger)
        {
            productService = _service;
            this.logger = logger;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<ProductService>>> GetAll()
        {
            try
            {
                var services = await productService.GetAllProducts();
                logger.LogInformation("Successful request");
                return Ok(services);
            }
            catch (KeyNotFoundException ex)
            {
                logger.LogInformation("Problem with DB");
                return NotFound(new { message = ex.Message });
            }
            catch (Exception ex)
            {
                logger.LogInformation("Server error");
                return StatusCode(StatusCodes.Status500InternalServerError, new { message = "An error occurred while processing your request." });
            }
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<AdminService>> GetProductById([FromHeader] int id)
        {
            try
            {
                var service = await productService.GetProductById(id);
                logger.LogInformation("Successful request");
                return Ok(service);
            }
            catch (KeyNotFoundException ex)
            {
                logger.LogInformation("Problem with DB");
                return NotFound(new { message = ex.Message });
            }
            catch (Exception ex)
            {
                logger.LogInformation("Server error");
                return StatusCode(StatusCodes.Status500InternalServerError, new { message = "An error occurred while processing your request." });
            }

        }

        [HttpPut("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> UpdateProduct([FromHeader] int id, [FromBody] Product _service)
        {
            try
            {
                var service = await productService.Update(id, _service);
                logger.LogInformation("Successful request");
                return Ok(service);
            }
            catch (KeyNotFoundException ex)
            {
                logger.LogInformation("Problem with DB");
                return NotFound(new { message = ex.Message });
            }
            catch (Exception ex)
            {
                logger.LogInformation("Server error");
                return StatusCode(StatusCodes.Status500InternalServerError, new { message = "An error occurred while processing your request." });
            }
        }


        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult<AdminService>> AddProduct([FromBody] Product _service)
        {
            try
            {
                var service = await productService.Save(_service);
                logger.LogInformation("Successful request");
                return Ok(service);
            }
            catch (KeyNotFoundException ex)
            {
                logger.LogInformation("Problem with DB");
                return NotFound(new { message = ex.Message });
            }
            catch (Exception ex)
            {
                logger.LogInformation("Server error");
                return StatusCode(StatusCodes.Status500InternalServerError, new { message = "An error occurred while processing your request." });
            }
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> DeleteProduct([FromHeader] int id)
        {
            try
            {
                var service = await productService.Delete(id);
                logger.LogInformation("Successful request");
                return Ok(service);
            }
            catch (KeyNotFoundException ex)
            {
                logger.LogInformation("Problem with DB");
                return NotFound(new { message = ex.Message });
            }
            catch (Exception ex)
            {
                logger.LogInformation("Server error");
                return StatusCode(StatusCodes.Status500InternalServerError, new { message = "An error occurred while processing your request." });
            }
        }

    }
}
