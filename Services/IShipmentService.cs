using System;
using System.Threading.Tasks;
using System.Collections.Generic;
using Shopping_Cart_Api.Models;
using Shopping_Cart_Api.ViewModels;

namespace Shopping_Cart_Api.Services
{
    public interface IShipmentService
    {
        Task<IEnumerable<ProductShipments>> GetAllShipment();
        Task<string> AddShipment(ProductShipments shipment);

        Task<IEnumerable<ShipmentProductQuantity>> GetShipmentProductQuantityById(Guid id);
    }
}