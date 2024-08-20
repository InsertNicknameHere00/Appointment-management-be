using AppointmentAPI.Data;
using AppointmentAPI.Entities;
using AppointmentAPI.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace AppointmentAPI.Services
{
    public class PromoCodeRepository : IPromoCodeRepository
    {
        private readonly HaircutSalonDbContext _context;

        public PromoCodeRepository(HaircutSalonDbContext context)
        {
            _context = context;
        }

        public async Task<List<string>> GeneratePromoCodes(int numberOfCodes, int discountPercentage)
        {
            var random = new Random();
            var generatedPromoCodes = new List<string>(); // List to store generated codes

            for (int i = 0; i < numberOfCodes; i++)
            {
                var promoCode = new PromoCodes
                {
                    Code = "PROMO" + random.Next(10000, 99999).ToString(), // 5-digit code
                    DiscountPercentage = discountPercentage,
                    ExpiryDate = DateTime.UtcNow.AddDays(10),
                    IsActive = true
                };

                _context.PromoCodes.Add(promoCode);
                generatedPromoCodes.Add(promoCode.Code); // Add the generated code to the list
            }

            await _context.SaveChangesAsync();

            return generatedPromoCodes; // Return the list of generated codes
        }


        public async Task<decimal> ApplyPromoCodeToOrder(int orderId, string promoCode)
        {
            Console.WriteLine($"OrderId being queried: {orderId}"); //finding errors
            var order = await _context.Orders
                                  .Where(o => o.OrderId == orderId)
                                  .SingleOrDefaultAsync();

            if (order == null)
                throw new Exception("Order not found");

           /*if (order.PromoCodeID != null)
                throw new Exception("A promo code has already been applied to this order");
            var promo = await _context.PromoCodes
                                .Where(pc => pc.Code == promoCode && pc.IsActive && (pc.ExpiryDate == null || pc.ExpiryDate >= DateTime.UtcNow))
                                .SingleOrDefaultAsync();
            if (promo == null)
                throw new Exception("Promo code is invalid or expired");

            var discount = order.TotalPrice * (promo.DiscountPercentage / 100);
            var discountedPrice = order.TotalPrice - discount;
            order.TotalPrice = discountedPrice;
            order.PromoCodeID = promo.PromoCodeID;

            await _context.SaveChangesAsync();

            return discountedPrice;*/
            return 5;
        }

    }
}