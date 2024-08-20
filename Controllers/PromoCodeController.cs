using AppointmentAPI.Repository.Interfaces;
using AppointmentAPI.Services;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace AppointmentAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PromoCodeController : ControllerBase
    {
        private readonly IPromoCodeRepository _promoCode;

        public PromoCodeController(IPromoCodeRepository promoCode)
        {
            _promoCode = promoCode;
        }

        [HttpPost("GeneratePromoCodes")]
        public async Task<IActionResult> GeneratePromoCodes([FromQuery] int numberOfCodes, [FromQuery] int discountPercentage)
        {
            try
            {
                var generatedCodes = await _promoCode.GeneratePromoCodes(numberOfCodes, discountPercentage);
                return Ok(new
                {
                    message = "Promo codes generated successfully with " + discountPercentage + "% discount",
                    codes = generatedCodes //shows the list of codes
                });
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("ApplyPromoCode")]
        public async Task<IActionResult> ApplyPromoCode([FromQuery] int orderId, [FromQuery] string promoCode)
        {
            try
            {
                //Console.WriteLine($"Received orderId: {orderId}, promoCode: {promoCode}"); for finding what is wrong

                var discountedPrice = await _promoCode.ApplyPromoCodeToOrder(orderId, promoCode);
                return Ok(new { discountedPrice });
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
