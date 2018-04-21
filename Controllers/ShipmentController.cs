using System;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Shopping_Cart_Api.Models;
using Shopping_Cart_Api.Services;
using Microsoft.AspNetCore.Authorization;

namespace Shopping_Cart_Api.Controllers
{
    [Route("api/[controller]")]

    public class ShipmentController : Controller
    {
        private readonly IShipmentService _shipmentService;
        public ShipmentController(IShipmentService shipmentService)
        {
            _shipmentService = shipmentService;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var isSuccessResult = await _shipmentService.GetAllShipment();
            if(isSuccessResult == null) return BadRequest();
            return Json(isSuccessResult);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(Guid id)
        {
            var isSuccessResult = await _shipmentService.GetShipmentProductQuantityById(id);
            if(isSuccessResult == null) return BadRequest();
            return Json(isSuccessResult);
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody]ProductShipments model)
        {
            var isSuccessResult = await _shipmentService.AddShipment(model);

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
    }
}