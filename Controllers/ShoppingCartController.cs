using AppointmentAPI.Entities;
using AppointmentAPI.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.DotNet.Scaffolding.Shared.Messaging;
using System.Security.Claims;

namespace AppointmentAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class ShoppingCartController : ControllerBase
    {
        private readonly IShoppingCartService shoppingCartService;
        private readonly IOrderService orderService;
        private readonly ILogger<ShoppingCartController> logger;

        public ShoppingCartController(IShoppingCartService shoppingCartService, ILogger<ShoppingCartController> logger, IOrderService _orderService)
        {
            this.shoppingCartService = shoppingCartService;
            this.orderService = _orderService;
            this.logger = logger;
        }

        [HttpPost]
        public async Task<IActionResult> AddProduct([FromBody] CartItem cartItem)
        {
            try
            {
                var userId = GetUserId();
                await shoppingCartService.AddProduct(userId, cartItem.Product, cartItem.Quantity);
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

        [HttpPost("Checkout{address}")]
        public async Task<IActionResult> Checkout([FromBody] string address)
        {
            try
            {
                var userId = GetUserId();
                var cartItems = await shoppingCartService.GetCartItems(userId);

                if (!cartItems.Any())
                {
                    return BadRequest("Cart is empty.");
                }

                var insufficientStock = orderService.CheckQuantity(cartItems);
                if (insufficientStock.Count() == 0)
                {
                    var result = await orderService.CreateOrderAsync(userId, cartItems, address);
                    await shoppingCartService.ClearCart(userId);
                    logger.LogInformation("Order is successfully created");
                    await orderService.SendEmail(result);
                    return Ok(result);
                }
                else
                {
                    var msg = "";
                    foreach (var product in insufficientStock)
                    {
                        logger.LogInformation(product.ProductName + " not enough quantity. Available: " + product.Quantity + " items.");
                        msg += product.ProductName + " not enough quantity. Available: " + product.Quantity + " items. \n ";
                        //return StatusCode(StatusCodes.Status500InternalServerError, new { message = product.ProductName + " not enough quantity. Available: " + product.Quantity + " items." });
                    }
                    return StatusCode(StatusCodes.Status500InternalServerError, new { message = msg });

                }

            }
            catch (KeyNotFoundException ex)
            {
                logger.LogInformation("Order is not successfully created");
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
                var result = await shoppingCartService.RemoveProduct(userId, productId);
                logger.LogInformation("Product is deleted successfully");
                return Ok(result);
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

        [HttpPut("{id}")]
        public async Task<ActionResult> UpdateOrder([FromHeader] int id, [FromBody] Order _order)
        {
            try
            {
                var order = await orderService.UpdateOrder(id, _order);
                logger.LogInformation("The order is successfully updated");
                await orderService.SendEmail(order);
                return Ok(order);
            }
            catch (KeyNotFoundException ex)
            {
                logger.LogInformation("Order is not found");
                return NotFound(new { message = ex.Message });
            }
            catch (Exception ex)
            {
                logger.LogInformation("Server error");
                return StatusCode(StatusCodes.Status500InternalServerError, new { message = "We are currently unable to process your request!" });
            }
        }

        [HttpPut("order/{id}")]
        public async Task<ActionResult> CancelOrder([FromHeader] int id, [FromBody] Order _order)
        {
            try
            {
                var order = await orderService.CancelOrder(id, _order);
                logger.LogInformation("The order is successfully cancelled.");
                await orderService.SendEmail(order);
                return Ok(order);
            }
            catch (KeyNotFoundException ex)
            {
                logger.LogInformation("Order is not found");
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
