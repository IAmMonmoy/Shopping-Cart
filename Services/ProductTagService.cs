using System;
using System.Threading.Tasks;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Shopping_Cart_Api.Data;
using Shopping_Cart_Api.Models;
using Shopping_Cart_Api.ViewModels;

namespace  Shopping_Cart_Api.Services
{
    public class ProductTagService : IProductTagService
    {
        private readonly ApplicationDbContext _context;
        public ProductTagService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<ProductTag>> GetAllProductTag()
        {
            return await _context.ProductTags.ToArrayAsync();
        }
    }
}