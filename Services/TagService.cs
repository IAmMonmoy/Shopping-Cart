using System;
using System.Threading.Tasks;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Shopping_Cart_Api.Data;
using Shopping_Cart_Api.Models;
using Shopping_Cart_Api.ViewModels;

namespace  Shopping_Cart_Api.Services
{
    public class TagService : ITagService
    {
        private readonly ApplicationDbContext _context;
        public TagService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Tag>> GetAllTag()
        {
            return await _context.Tags.ToArrayAsync();
        }

        public async Task<Tag> GetTagById(Guid id)
        {
            return await _context.Tags.FindAsync(id);
        }
        
        public async Task<string> AddTag(TagViewModel tag)
        {
            Tag entity = new Tag
            {
                Id = new Guid(),
                TagName = tag.TagName,
                TagDescription = tag.TagDescription
            };
            _context.Tags.Add(entity);
            bool success = await _context.SaveChangesAsync() == 1;
            if(success) return entity.Id.ToString();
            else return "Unsucessfull" ;
        }

        public async Task<string> EditTagById(Guid id,TagViewModel tag)
        {
            var entity = await _context.Tags.FindAsync(id);
            entity.TagName = tag.TagName;
            entity.TagDescription = tag.TagDescription;
            
            bool success = await _context.SaveChangesAsync() >= 1;
            if(success) return entity.Id.ToString();
            else return "Unsucessfull" ;
        }

        public async Task<bool> DeleteTagById(Guid id)
        {
            _context.Tags.Remove(await _context.Tags.FindAsync(id));
            return 1 == await _context.SaveChangesAsync();
        }
    }
}