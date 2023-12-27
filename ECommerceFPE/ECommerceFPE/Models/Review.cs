using System.ComponentModel.DataAnnotations;

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
        [Required]
        public int CustomerId { get; set; }
        public Customer Customer { get; set; }

        [Required]
        public int ProductId { get; set; }
        public ProductCatalog ProductCatalog { get; set; }
    }
}