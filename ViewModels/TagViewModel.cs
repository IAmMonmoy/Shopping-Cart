using System;
using System.ComponentModel.DataAnnotations;

namespace Shopping_Cart_Api.Services
{
    public class TagViewModel
    {
        [Required]
        [StringLength(100,ErrorMessage="The {0} must be between {2} and {1} length",MinimumLength=2)]
        public string TagName { get; set; }

        [Required]
        [StringLength(1000,ErrorMessage="The {0} must be between {2} and {1} length",MinimumLength=2)]
        public string TagDescription { get; set; }
    }
}