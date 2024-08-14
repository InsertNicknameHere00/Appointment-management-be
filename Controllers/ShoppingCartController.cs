using AppointmentAPI.Entities;
using AppointmentAPI.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace AppointmentAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class ShoppingCartController : ControllerBase
    {
        private readonly IShoppingCartService shoppingCartService;
        private readonly ILogger<ShoppingCartController> logger;

        public ShoppingCartController(IShoppingCartService shoppingCartService, ILogger<ShoppingCartController> logger)
        {
            this.shoppingCartService = shoppingCartService;
            this.logger = logger;
        }

        [HttpPost]
        public async Task<IActionResult> AddProduct([FromBody] CartItem cartItem)
        {
            try
            {
                var userId = GetUserId();
                await shoppingCartService.AddProduct(userId,cartItem.Product, cartItem.Quantity);
                logger.LogInformation("Product is successfully added");
                return Ok(cartItem);
            }
            catch (KeyNotFoundException ex)
            {
                logger.LogInformation("Product is not added successfully");
                return NotFound(new { message = ex.Message });
            }
            catch (Exception ex)
            {
                logger.LogInformation("Server error");
                return StatusCode(StatusCodes.Status500InternalServerError, new { message = "We are currently unable to process your request!" });
            }
        }


        [HttpDelete("{productId}")]
        public async Task<IActionResult> DeleteProduct([FromHeader] int productId)
        {
            try
            {
                var userId = GetUserId();
                await shoppingCartService.RemoveProduct(userId,productId);
                logger.LogInformation("Product is deleted successfully");
                return Ok();
            }
            catch (KeyNotFoundException ex)
            {
                logger.LogInformation("Product is not found");
                return NotFound(new { message = ex.Message });
            }
            catch (Exception ex)
            {
                logger.LogInformation("Server error");
                return StatusCode(StatusCodes.Status500InternalServerError, new { message = "We are currently unable to process your request!" });
            }


        }

        [HttpGet("Total price")]
        public async Task<IActionResult> GetTotalPrice()
        {
            try
            {
                var userId = GetUserId();
                decimal totalPrice = await shoppingCartService.TotalPrice(userId);
                logger.LogInformation("Total price");
                return Ok(totalPrice);
            }
            catch (KeyNotFoundException ex)
            {
                logger.LogInformation("Missing total price");
                return NotFound(new { message = ex.Message });
            }
            catch (Exception ex)
            {
                logger.LogInformation("Server error");
                return StatusCode(StatusCodes.Status500InternalServerError, new { message = "We are currently unable to process your request!" });
            }
        }

        [HttpGet("Products in the cart")]
        public async Task<ActionResult<IEnumerable<CartItem>>> GetProducts()
        {
            try
            {
                var userId = GetUserId();
                var cart = await shoppingCartService.GetCartItems(userId);
                logger.LogInformation("All products");
                return Ok(cart);
            }
            catch (KeyNotFoundException ex)
            {
                logger.LogInformation("Cart is empty");
                return NotFound(new { message = ex.Message });
            }
            catch (Exception ex)
            {
                logger.LogInformation("Server error");
                return StatusCode(StatusCodes.Status500InternalServerError, new { message = "We are currently unable to process your request!" });
            }
        }

        private int GetUserId()
        {
            return int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value);
        }
    }
}
