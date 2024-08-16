using AppointmentAPI.Entities;

namespace AppointmentAPI.Services.Interfaces
{
    public interface IShoppingCartService
    {
        Task<IEnumerable<CartItem>> GetCartItems(int userId);
        Task<Dictionary<int, List<CartItem>>> AddProduct(int userId, Product product, int quantity);
        Task<bool> RemoveProduct(int userId, int id);
        Task<decimal> TotalPrice(int userId);
        Task<bool> ClearCart(int userId);


    }
}
