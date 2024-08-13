using AppointmentAPI.Entities;

namespace AppointmentAPI.Services
{
    public interface IProductService
    {
        Task<List<Product>> GetAllProducts();
        Task<Product> GetProductById(int id);
        Task<Product> Save(Product product);

        Task<Product> Update(int id, Product product);
        Task<bool> Delete(int id);
    }
}
