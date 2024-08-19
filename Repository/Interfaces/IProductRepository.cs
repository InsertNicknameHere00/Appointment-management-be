using AppointmentAPI.Entities;

namespace AppointmentAPI.Repository.Interfaces
{
    public interface IProductRepository
    {
        Task<List<Product>> GetAllProducts();
        Task<Product> GetProductById(int serviceId);
        Task<Product> AddProduct(Product product);
        Task<Product> UpdateProduct(int productId, Product product);
        Task<bool> DeleteProduct(Product product);

        Product Search(int id);
    }
}
