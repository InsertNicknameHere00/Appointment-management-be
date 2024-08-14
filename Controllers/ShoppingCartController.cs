using AppointmentAPI.Entities;
using AppointmentAPI.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace AppointmentAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ShoppingCartController : ControllerBase
    {
        private readonly IShoppingCartService shoppingCartService;
        private readonly ILogger<ShoppingCartController> logger;
        //private readonly List<CartItem> cart = new List<CartItem>();

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
                await shoppingCartService.AddProduct(cartItem.Product, cartItem.Quantity);
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
                await shoppingCartService.RemoveProduct(productId);
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
                decimal totalPrice = await shoppingCartService.TotalPrice();
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
                var cart = await shoppingCartService.GetCartItems();
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
    }
}
