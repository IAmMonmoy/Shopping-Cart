using System;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Shopping_Cart_Api.ViewModels;
using Shopping_Cart_Api.Services;
using Shopping_Cart_Api.Models;
using Microsoft.AspNetCore.Authorization;

namespace Shopping_Cart_Api.Controllers
{
    [Authorize(Policy = nameof(Constants.AdministratorRole))]
    [Route("api/[controller]")]
    public class TagsController : Controller
    {
        private readonly ITagService _tagService;
        public TagsController(ITagService tagService)
        {
            _tagService = tagService;
        }
        
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var isSuccessResult = await _tagService.GetAllTag();
            if(isSuccessResult == null) return BadRequest();
            return Json(isSuccessResult);
        }

        [HttpGet("{id}", Name = "TagGet")]
        public async Task<IActionResult> Get(Guid id)
        {
            var isSuccessResult = await _tagService.GetTagById(id);
            if(isSuccessResult == null) return BadRequest();
            return Json(isSuccessResult);
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody]TagViewModel model)
        {
            var isSuccessResult = await _tagService.AddTag(model);

            if(isSuccessResult == "Unsucessfull")
                return BadRequest();
            else 
            {
                var NewUri = Url.Link("TagGet",new{id = new Guid(isSuccessResult)});
                return Created(NewUri,model);
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put(Guid id , [FromBody]TagViewModel model)
        {
            var isSuccessResult = await _tagService.EditTagById(id,model);

            if(isSuccessResult == "Unsucessfull")
                return BadRequest();
            else 
            {
                var NewUri = Url.Link("TagGet",new{id = new Guid(isSuccessResult)});
                return Created(NewUri,model);
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var isSuccessResult = await _tagService.DeleteTagById(id);

            if(!isSuccessResult)
                return BadRequest();
            else 
            {
                return Ok();
            }
        }
    }
}