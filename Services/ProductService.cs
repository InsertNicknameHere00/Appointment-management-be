using AppointmentAPI.Entities;
using AppointmentAPI.Repository.Interfaces;
using AppointmentAPI.Services.Interfaces;
using NuGet.Protocol.Core.Types;

namespace AppointmentAPI.Services
{
    public class ProductService : IProductService
    {
        private readonly IProductRepository _repository;
        public ProductService(IProductRepository productRepository)
        {
            _repository = productRepository;
        }

        public async Task<bool> Delete(int id)
        {
            var result = _repository.Search(id);
            if (result != null)
            {
                await _repository.DeleteProduct(result);
                return true;
            }
            else
            {
                throw new KeyNotFoundException();
            }
        }

        public async Task<List<Product>> GetAllProducts()
        {
            var result = await _repository.GetAllProducts();
            if (result.Count != 0)
            {
                return result;
            }
            else
            {
                throw new KeyNotFoundException();
            }
        }

        public async Task<Product> GetProductById(int id)
        {
            var result = await _repository.GetProductById(id);
            if (result != null)
            {
                return result;
            }
            else
            {
                throw new KeyNotFoundException();
            }
        }

        public async Task<Product> Save(Product product)
        {
            if (product != null)
            {
                var result = new Product();
                result.ProductId = product.ProductId;
                result.ProductName = product.ProductName;
                result.ProductDescription = product.ProductDescription;
                result.Price=product.Price;
                result.Quantity=product.Quantity;
                result.ProductImage = product.ProductImage;              
                await _repository.AddProduct(result);

                return result;

            }
            else
            {
                throw new KeyNotFoundException();
            }
        }

        public async Task<Product> Update(int id, Product product)
        {
            var result = await _repository.GetProductById(id);
            if (product != null)
            {
                result.ProductId = product.ProductId;
                result.ProductName = product.ProductName;
                result.ProductDescription = product.ProductDescription;
                result.Price = product.Price;
                result.Quantity = product.Quantity;
                result.ProductImage = product.ProductImage;
                await _repository.UpdateProduct(id,result);

                return result;

            }
            else
            {
                throw new KeyNotFoundException();
            }
        }
    }
}
