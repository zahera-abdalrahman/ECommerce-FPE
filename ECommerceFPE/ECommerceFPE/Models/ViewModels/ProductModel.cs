using System.ComponentModel.DataAnnotations;

namespace ECommerceFPE.Models.ViewModels
{
    public class ProductModel
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

        public IFormFile File { get; set; }
    }
}
