using System;
using System.ComponentModel.DataAnnotations;

namespace Shopping_Cart_Api.Models
{
    public class Shipment
    {
        public ApplicationUser User { get; set; }

        [Required]
        public string UserId { get; set; }

        public Product Product { get; set; }

        [Required]
        public Guid ProductId { get; set; }
    }
}