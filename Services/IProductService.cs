using System;
using System.Threading.Tasks;
using System.Collections.Generic;
using Shopping_Cart_Api.Models;

namespace Shopping_Cart_Api.Services
{
    public interface IProductService
    {
        Task<IEnumerable<Product>> GetAllProduct();
        Task<Product> GetProductById(Guid id);
        Task<bool> AddProduct();
        Task<bool> EditProduct();
        Task<bool> DeleteProduct(Guid id);
    }
}