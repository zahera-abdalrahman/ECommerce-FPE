using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ECommerceFPE.Models
{
    public class ReviewAll
    {
        [Key]
        public int ReviewId { get; set; }

        // Foreign Key for User
        public string UserId { get; set; }

        public ApplicationUser ApplicationUser { get; set; }
        [Required(ErrorMessage = "Please enter your review.")]
        public string Comment { get; set; }

        public DateTime ReviewDate { get; set; }

        public bool isActive { get; set; }
    }
}
