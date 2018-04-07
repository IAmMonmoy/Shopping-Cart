using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Http;

namespace Shopping_Cart_Api.ViewModels
{
    public class ProductViewModel
    {
        [Required]
        [MaxLength(50)]
        public string ProductCode { get; set; }

        [Required]
        [StringLength(50,ErrorMessage="The max product length must be between {2} and {1}", MinimumLength = 2)]
        public string ProductName { get; set; }

        [Required]
        [StringLength(500,ErrorMessage="The max description length must be between {2} and {1}", MinimumLength = 2)]
        public string Description { get; set; }
        
        [Required]
        [DataType(DataType.Currency,ErrorMessage = "The price must be currency")]
        public double Price { get; set; }

        [Required]
        [Range(0,5000,ErrorMessage="The value must be between {0} and {1}")]
        public int Stock { get; set; }

        [Required]
        public List<IFormFile> Image { get; set; }

        [Required]
        public List<Guid> Tags { get; set; }
    }
}