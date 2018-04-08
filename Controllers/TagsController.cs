using System;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Shopping_Cart_Api.ViewModels;
using Shopping_Cart_Api.Services;

namespace Shopping_Cart_Api.Controllers
{
    [Route("api/[controller]")]
    public class TagsController : Controller
    {
        private readonly ITagService _tagService;
        public TagsController(ITagService tagService)
        {
            _tagService = tagService;
        }

        [HttpGet("{id}", Name = "TagGet")]
        public async Task<IActionResult> Get(Guid id)
        {
            var isSuccessResult = await _tagService.GetTagById(id);
            if(isSuccessResult == null) return BadRequest("The Request was Unsuccessfull");
            return Json(isSuccessResult);
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody]TagViewModel model)
        {
            var isSuccessResult = await _tagService.AddTag(model);

            if(isSuccessResult == "Unsucessfull")
                return BadRequest("The Request was Unsuccessfull");
            else 
            {
                var NewUri = Url.Link("TagGet",new{id = new Guid(isSuccessResult)});
                return Created(NewUri,model);
            }
        }
    }
}