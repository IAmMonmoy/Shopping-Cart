using System;
using System.ComponentModel.DataAnnotations;

namespace Shopping_Cart_Api.Models
{
    public class SoldList
    {
        public Guid Id { get; set; }

        public Product Product { get; set; }

        [Required]
        public Guid ProductId { get; set; }

        [Required]
        public int Amount { get; set; }

        [Required]
        public double TotalPrice { get; set; }

        [Required]
        public DateTime Date { get; set; }
    }
}