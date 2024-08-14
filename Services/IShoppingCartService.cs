using AppointmentAPI.Entities;

namespace AppointmentAPI.Services
{
    public interface IShoppingCartService
    {
        Task<IEnumerable<CartItem>> GetCartItems(int userId);
        Task AddProduct(int userId,Product product, int quantity);
        Task RemoveProduct(int userId,int id);
        Task<decimal> TotalPrice(int userId);
        Task ClearCart(int userId);
    }
}
