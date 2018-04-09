using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Shopping_Cart_Api.ViewModels;
using Shopping_Cart_Api.Models;

namespace Shopping_Cart_Api.Services
{
    public interface ITagService
    {
        Task<IEnumerable<Tag>> GetAllTag();
        Task<Tag> GetTagById(Guid id);
        Task<string> AddTag(TagViewModel tag);
        Task<string> EditTagById(Guid id, TagViewModel tag);
        Task<bool> DeleteTagById(Guid id);
    }
}