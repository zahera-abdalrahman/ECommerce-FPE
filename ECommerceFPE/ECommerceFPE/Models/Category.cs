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

<<<<<<< Updated upstream:ECommerceFPE/ECommerceFPE/Models/ProductCategory.cs
        // Collection navigation properties
        public ICollection<ProductCatalog> ProductCatalogs { get; set; }
=======
        [Required]
        public string ImageUrl { get; set; }
        public bool IsDeleted { get; set; }

>>>>>>> Stashed changes:ECommerceFPE/ECommerceFPE/Models/Category.cs
    }
}