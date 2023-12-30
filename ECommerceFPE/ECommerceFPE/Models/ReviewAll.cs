using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ECommerceFPE.Models
{
    public class ReviewAll
    {
        [Key]
        public int ReviewId { get; set; }

        // Foreign Key for User
        [ForeignKey("Customer")]
        public int CustomerId { get; set; }
        public Customer Customer { get; set; }
        [Required]
        public string Comment { get; set; }
        [Required]
        public int Rating { get; set; }
        
        public DateTime ReviewDate { get; set; }

        public bool isActive { get; set; }
    }
}
