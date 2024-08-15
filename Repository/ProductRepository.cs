using AppointmentAPI.Data;
using AppointmentAPI.Entities;
using Microsoft.EntityFrameworkCore;

namespace AppointmentAPI.Repository
{
    public class ProductRepository : IProductRepository
    {
        private readonly HaircutSalonDbContext _context;
        public ProductRepository(HaircutSalonDbContext context)
        {
            _context = context;

        }
        public async Task<Product> AddProduct(Product product)
        {
             await _context.Product.AddAsync(product);
             await _context.SaveChangesAsync();
            return product;
        }

        public async Task<bool> DeleteProduct(Product product)
        {
            _context.Product.Remove(product);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<List<Product>> GetAllProducts()
        {
            return await _context.Product.ToListAsync();
        }

        public async Task<Product> GetProductById(int productId)
        {
            return await _context.Product.FindAsync(productId);
        }

        public Product Search(int id)
        {
            var products = _context.Product.Find(id);
            return products;
        }

        public async Task<Product> UpdateProduct(int productId, Product product)
        {
            _context.Product.Update(product);
            await _context.SaveChangesAsync();
            return product;
        }

    }
}
