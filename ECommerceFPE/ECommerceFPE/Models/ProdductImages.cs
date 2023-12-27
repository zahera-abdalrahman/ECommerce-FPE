using System.ComponentModel.DataAnnotations;

namespace ECommerceFPE.Models
{
    public class ProductImages
    {
        [Key]
        public int ImageId { get; set; }

        [Required]
        public string ImageUrl { get; set; }

        [Required]
        public int ImageOrder { get; set; }

        // Foreign Keys
        [Required]
        public int ProductId { get; set; }
        public ProductCatalog ProductCatalog { get; set; }
    }
}