using System;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using Shopping_Cart_Api.Models;
using Shopping_Cart_Api.Data;

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

        public Task<bool> AddProduct()
        {
            throw new NotImplementedException();
        }

        public Task<bool> EditProduct()
        {
            throw new NotImplementedException();
        }

        public async Task<bool> DeleteProduct(Guid id)
        {
            _context.Products.Remove(await _context.Products.FindAsync(id));
            return await _context.SaveChangesAsync() == 1;
        }
    }
}