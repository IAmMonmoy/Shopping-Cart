using System;
using System.ComponentModel.DataAnnotations;

namespace Shopping_Cart_Api.Models
{
    public class ShipmentProductQuantity
    {
        public Guid Id { get; set; }

        public string ProductName { get; set; }

        public int Quantity { get; set; }
        
        public Shipments Shipments { get; set; }
        
        [Required]
        public Guid ShipmentsId { get; set; }
    }
}