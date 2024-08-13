using AppointmentAPI.Entities;
using Microsoft.CodeAnalysis;

namespace AppointmentAPI.Services
{
    public class ShoppingCartService : IShoppingCartService
    {
        List<CartItem> products;
        public ShoppingCartService() { 
            products = new List<CartItem>();
        }

        public Task AddProduct(Product product, int quantity)
        {
            var existingCartItem = products.FirstOrDefault(ci => ci.Product.ProductId == product.ProductId);

            if (existingCartItem != null)
            {
                // If the product already exists in the cart, update the quantity
                existingCartItem.Quantity += quantity;
            }
            else
            {
                // Otherwise, add a new CartItem to the cart
                products.Add(new CartItem { Product = product, Quantity = quantity });
            }

            return Task.CompletedTask;
        }

        public Task ClearCart()
        {
            products.Clear();
            return Task.CompletedTask;
        }

        public Task<List<CartItem>> GetCartItems()
        {
            return Task.FromResult<List<CartItem>>(products);
        }

        public Task RemoveProduct(int id)
        {
            var existingCartItem = products.FirstOrDefault(ci => ci.Product.ProductId == id);

            if (existingCartItem != null)
            {
                products.Remove(existingCartItem);
            }

            return Task.CompletedTask;
        }

        public Task<decimal> TotalPrice()
        {
            var totalPrice = products.Sum(p => p.Product.Price * p.Quantity);
            return Task.FromResult(totalPrice);
        }
    }
}
