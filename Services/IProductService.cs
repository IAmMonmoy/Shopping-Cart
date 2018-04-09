using System;
using System.Threading.Tasks;
using System.Collections.Generic;
using Shopping_Cart_Api.Models;
using Shopping_Cart_Api.ViewModels;

namespace Shopping_Cart_Api.Services
{
    public interface IProductService
    {
        Task<IEnumerable<Product>> GetAllProduct();
        Task<Product> GetProductById(Guid id);
        Task<string> AddProduct(ProductViewModel tag);
        Task<string> EditProductById(Guid id, ProductViewModel tag);
        Task<bool> DeleteProductById(Guid id);
    }
}