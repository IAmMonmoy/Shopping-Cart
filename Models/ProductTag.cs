using System;
using System.ComponentModel.DataAnnotations;
namespace Shopping_Cart_Api.Models
{
    public class ProductTag
    {
        [Required]
        public Guid ProductId { get; set; }
        public Product Product { get; set; }
        
        [Required]
        public Guid TagId { get; set; }
        
        public Tag Tag { get; set; }
    }
}