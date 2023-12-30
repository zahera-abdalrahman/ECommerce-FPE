﻿using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ECommerceFPE.Models
{
    public class Product
    {
        [Key]
        public int ProductId { get; set; }

        [Required]
        public string ProductName { get; set; }

        [Required]
        public string Description { get; set; }

        [Required]
        public double Price { get; set; }

        [Required]
        public int QuantityInStock { get; set; }
        public int CategoryId { get; set; }
        public Category Category { get; set; }

        public int DiscountPercent { get; set; }

        [Required]
        public string ImageUrl { get; set; }
        
    }
}