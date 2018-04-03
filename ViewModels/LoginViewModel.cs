using System;
using System.ComponentModel.DataAnnotations;

namespace Shopping_Cart_Api.ViewModels
{
    public class LoginViewModel
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} and at most {1} characters long.", MinimumLength = 5)]
        [DataType(DataType.Password)]
        public string Password { get; set; }
    }
}