using AppointmentAPI.Entities;

namespace AppointmentAPI.Services
{
    public interface IShoppingCartService
    {
        Task<List<CartItem>> GetCartItems();
        Task AddProduct(Product product, int quantity);
        Task RemoveProduct(int id);
        Task<decimal> TotalPrice();
        Task ClearCart();
    }
}
