using AppointmentAPI.Entities;

namespace AppointmentAPI.Services
{
    public class ProductService : IProductService
    {
        public Task<bool> Delete(int id)
        {
            throw new NotImplementedException();
        }

        public Task<List<Product>> GetAllProducts()
        {
            throw new NotImplementedException();
        }

        public Task<Product> GetProductById(int id)
        {
            throw new NotImplementedException();
        }

        public Task<Product> Save(Product product)
        {
            throw new NotImplementedException();
        }

        public Task<Product> Update(int id, Product product)
        {
            throw new NotImplementedException();
        }
    }
}
