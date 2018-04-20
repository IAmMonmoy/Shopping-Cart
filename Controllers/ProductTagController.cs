using System;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Shopping_Cart_Api.ViewModels;
using Shopping_Cart_Api.Services;
using Microsoft.AspNetCore.Authorization;

namespace Shopping_Cart_Api.Controllers
{
    [Route("api/[controller]")]
    public class ProductTagsController : Controller
    {
        private readonly IProductTagService _productTagService;
        public ProductTagsController(IProductTagService productTagService)
        {
            _productTagService = productTagService;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var isSuccessResult = await _productTagService.GetAllProductTag();
            if(isSuccessResult == null) return BadRequest("The Request was Unsuccessfull");
            return Json(isSuccessResult);
        }
    }
}