using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Shopping_Cart_Api.ViewModels;
using Shopping_Cart_Api.Models;

namespace Shopping_Cart_Api.Services
{
    public interface ITagService
    {
        Task<Tag> GetTagById(Guid id);
        Task<string> AddTag(TagViewModel tag);
    }
}