using System;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;

namespace Shopping_Cart_Api.Models
{
    public class Shipments
    {
        [Required]
        public Guid Id { get; set; }
        
        [Required]
        public string UserName { get; set; }

        [Required]
        public string BuyerName { get; set; }

        [Required]
        public string BuyerAddress { get; set; }

        [Required]
        public string BuyerPhone { get; set; }

        [Required]
        public double TotalCost { get; set; }

        [Required]
        public bool isDelivered { get; set; }

        public List<ShipmentProductQuantity> productQuantity;
    }
}