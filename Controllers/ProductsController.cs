using System;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Shopping_Cart_Api.ViewModels;
using Shopping_Cart_Api.Services;
using Microsoft.AspNetCore.Authorization;

namespace Shopping_Cart_Api.Controllers
{
    [Route("api/[controller]")]
    public class ProductsController : Controller
    {
        private readonly IProductService _productService;
        public ProductsController(IProductService productService)
        {
            _productService = productService;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var isSuccessResult = await _productService.GetAllProduct();
            if(isSuccessResult == null) return BadRequest();
            return Json(isSuccessResult);
        }

        [HttpGet("{id}", Name = "ProductGet")]
        public async Task<IActionResult> Get(Guid id)
        {
            var isSuccessResult = await _productService.GetProductById(id);
            if(isSuccessResult == null) return BadRequest();
            return Json(isSuccessResult);
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromForm]ProductViewModel model)
        {
            var isSuccessResult = await _productService.AddProduct(model);

            //check if returned result is guid or not
            //if guid it was successfull. Otherwise unsuccessfull
            Guid GuidOutput;
            bool isGuid = Guid.TryParse(isSuccessResult,out GuidOutput);

            if(!isGuid)
                return BadRequest(isSuccessResult);
            else 
            {
                //var NewUri = Url.Link("ProductGet",new{id = new Guid(isSuccessResult)});
                //return Created(NewUri,model);
                return Ok();
            }
        }

        [HttpPut("{id}/{stock}")]
        public async Task<IActionResult> Put(Guid id , int stock)
        {
            var isSuccessResult = await _productService.EditProductById(id,stock);

            if(isSuccessResult == "Unsucessfull")
                return BadRequest();
            else 
            {
                return Ok();
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var isSuccessResult = await _productService.DeleteProductById(id);

            if(!isSuccessResult)
                return BadRequest();
            else 
            {
                return Ok();
            }
        }
    }
}