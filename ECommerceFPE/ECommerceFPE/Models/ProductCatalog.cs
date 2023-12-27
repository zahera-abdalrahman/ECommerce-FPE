using System.ComponentModel.DataAnnotations;

namespace ECommerceFPE.Models
{
    public class ProductCatalog
    {
        [Key]
        [Required]
        public int ProductId { get; set; }

        [Required]
        public string ProductName { get; set; }

        [Required]
        public string Description { get; set; }

        [Required]
        public double Price { get; set; }

        [Required]
        public int QuantityInStock { get; set; }

        //Foreign Keys
        [Required]
        public int CategoryId { get; set; }
        public ProductCategory ProductCategory { get; set; }

        public decimal DiscountPercent { get; set; }

        // Collection navigation properties
        public ICollection<ProductImages> ProductImages { get; set; }
        public ICollection<OrderItems> OrderItems { get; set; }
        public ICollection<Review> Reviews { get; set; }
    }
}