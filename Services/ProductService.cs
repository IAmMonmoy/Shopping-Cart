using System;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using Shopping_Cart_Api.Models;
using Shopping_Cart_Api.Data;
using Shopping_Cart_Api.ViewModels;

namespace Shopping_Cart_Api.Services
{
    public class ProductService : IProductService
    {
        private readonly ApplicationDbContext _context;
        public ProductService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Product>> GetAllProduct()
        {
            return await _context.Products.ToArrayAsync();
        }

        public async Task<Product> GetProductById(Guid id)
        {
            return await _context.Products.FindAsync(id);
        }
        
        public async Task<bool> DeleteProduct(Guid id)
        {
            _context.Products.Remove(await _context.Products.FindAsync(id));
            return await _context.SaveChangesAsync() == 1;
        }

        public Task<string> AddProduct(ProductViewModel tag)
        {
            throw new NotImplementedException();
        }

        public Task<string> EditProductById(Guid id, ProductViewModel tag)
        {
            throw new NotImplementedException();
        }

        public Task<bool> DeleteProductById(Guid id)
        {
            throw new NotImplementedException();
        }
    }
}