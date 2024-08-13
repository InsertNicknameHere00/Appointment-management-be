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
             await _context.Products.AddAsync(product);
             await _context.SaveChangesAsync();
            return product;
        }

        public async Task<bool> DeleteProduct(Product product)
        {
            _context.Products.Remove(product);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<List<Product>> GetAllProducts()
        {
            return await _context.Products.ToListAsync();
        }

        public async Task<Product> GetProductById(int productId)
        {
            return await _context.Products.FindAsync(productId);
        }

        public Product Search(int id)
        {
            var products = _context.Products.Find(id);
            return products;
        }

        public async Task<Product> UpdateProduct(int productId, Product product)
        {
            _context.Products.Update(product);
            await _context.SaveChangesAsync();
            return product;
        }
    }
}
