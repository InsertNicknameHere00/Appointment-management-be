using AppointmentAPI.Entities;
using Microsoft.CodeAnalysis;

namespace AppointmentAPI.Services
{
    public class ShoppingCartService : IShoppingCartService
    {
        Dictionary<int,List<CartItem>> cart ;
        public ShoppingCartService() { 
            cart = new Dictionary<int, List<CartItem>>();
        }

        public Task AddProduct(int userId,Product product, int quantity)
        {
            if (!cart.ContainsKey(userId))
            {
                cart[userId] = new List<CartItem>();
            }

            var existingCartItem = cart[userId].FirstOrDefault(ci => ci.Product.ProductId == product.ProductId);

            if (existingCartItem != null)
            {
                // If the product already exists in the cart, update the quantity
                existingCartItem.Quantity += quantity;
            }
            else
            {
                // Otherwise, add a new CartItem to the cart
                cart[userId].Add(new CartItem { Product = product, Quantity = quantity });
            }

            return Task.CompletedTask;
        }

        public Task ClearCart(int userId)
        {
            if (cart.ContainsKey(userId))
            {
                cart[userId].Clear();
            }
            return Task.CompletedTask;
        }

        public Task<IEnumerable<CartItem>> GetCartItems(int userId)
        {
            if (cart.ContainsKey(userId))
            {
                return Task.FromResult<IEnumerable<CartItem>>(cart[userId]);
            }

            return Task.FromResult<IEnumerable<CartItem>>(new List<CartItem>());
        }

        public Task RemoveProduct(int userId,int id)
        {
            if (cart.ContainsKey(userId))
            {
                var existingCartItem = cart[userId].FirstOrDefault(ci => ci.Product.ProductId == id);
                if (existingCartItem != null)
                {
                    cart[userId].Remove(existingCartItem);
                }
            }


            return Task.CompletedTask;
        }

        public Task<decimal> TotalPrice(int userId)
        {
            if (cart.ContainsKey(userId))
            {
                var totalPrice = cart[userId].Sum(p => p.Product.Price * p.Quantity);
                return Task.FromResult(totalPrice);
            }

            return Task.FromResult(0m);
        }
    }
}
