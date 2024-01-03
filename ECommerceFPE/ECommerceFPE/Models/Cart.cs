using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ECommerceFPE.Models
{
    public class Cart
    {
        [Key]
        public int CartId { get; set; }


        [ForeignKey("ApplicationUser")]
        public string UserId { get; set; }

        public ApplicationUser ApplicationUser { get; set; }

        [Required]
        public double Total { get; set; }
    }
}
