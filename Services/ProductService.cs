using System;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using Shopping_Cart_Api.Models;
using Shopping_Cart_Api.Data;
using Shopping_Cart_Api.ViewModels;
using System.IO;
using System.Linq;

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
            return await _context.Products.Include(pro => pro.Tags).Include( pro => pro.Image).ToArrayAsync();
        }

        public async Task<Product> GetProductById(Guid id)
        {
            return await _context.Products.Include(pro => pro.Tags).Include( pro => pro.Image).Where(m=> m.Id == id).SingleAsync();
        }
        
        public async Task<bool> DeleteProduct(Guid id)
        {
            _context.Products.Remove(await _context.Products.FindAsync(id));
            return await _context.SaveChangesAsync() == 1;
        }

        public async Task<string> AddProduct(ProductViewModel product)
        {
            var allowedExtensions = new[] { ".jpg", ".jpeg", ".png" };
            var message = "";

             var entity = new Product
            {
                Id = new Guid(),
                ProductCode = product.ProductCode,
                ProductName = product.ProductName,
                Description = product.Description,
                Price = product.Price,
                Stock = product.Stock
            };

             _context.Products.Add(entity);

            foreach (var img in product.Image)
            {
                var extention = Path.GetExtension(img.FileName);
                if(allowedExtensions.Contains(extention.ToLower()) || img.Length > 2000000)
                    message = "Select jpg or jpeg or png less than 2Μ";
                var fileName = Path.Combine("Products",DateTime.Now.Ticks+extention);
                var path = Path.Combine(Directory.GetCurrentDirectory(),"wwwroot",fileName);
               
                try{
                    using(var stream = new FileStream(path,FileMode.Create))
                    {
                        await img.CopyToAsync(stream);
                    }
                }
                catch{
                    return "can not upload image";
                }

                var imageEntity = new Image
                {
                    Id = new Guid(),
                    ProductId = entity.Id,
                    Path = fileName
                };

                _context.Images.Add(imageEntity);
            }

            foreach (var tag in product.Tags)
            {
                var tagEntity = new ProductTag
                {
                    ProductId = entity.Id,
                    TagId = tag
                };
                _context.ProductTags.Add(tagEntity);
            }

             bool success = await _context.SaveChangesAsync() == 1+product.Image.Count+product.Tags.Count;

            if(success) return entity.Id.ToString();
            else return message;
           
        }

        public Task<string> EditProductById(Guid id, ProductViewModel tag)
        {
            throw new NotImplementedException();
        }

        public async Task<bool> DeleteProductById(Guid id)
        {
            _context.Products.Remove(await _context.Products.FindAsync(id));
            return 1 == await _context.SaveChangesAsync();
        }
    }
}