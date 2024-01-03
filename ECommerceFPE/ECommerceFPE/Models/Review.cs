using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ECommerceFPE.Models
{
    public class Review
    {
        [Key]
        [Required]
        public int ReviewId { get; set; }

        [Required]
        [DataType(DataType.MultilineText)]
        public string ReviewText { get; set; }

        [Required]
        [Range(1, 5, ErrorMessage = "Rating must be between 1 and 5")]
        public int Rating { get; set; }

        [Required]
        public DateTime ReviewDate { get; set; }

        // Foreign Keys
        public string UserId { get; set; }

        public ApplicationUser ApplicationUser { get; set; }

        [Required]
        [ForeignKey("Product")]
        public int ProductId { get; set; }
        public Product ProductCatalog { get; set; }
    }
}