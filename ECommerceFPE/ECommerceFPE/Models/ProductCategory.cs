using System.ComponentModel.DataAnnotations;

namespace ECommerceFPE.Models
{
    public class ProductCategory
    {
        [Key]
        [Required]
        public int CategoryId { get; set; }

        [Required]
        public string CategoryName { get; set; }

        // Collection navigation properties
        public ICollection<ProductCatalog> ProductCatalogs { get; set; }
    }
}