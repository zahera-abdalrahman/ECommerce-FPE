using System.ComponentModel.DataAnnotations;

namespace ECommerceFPE.Models
{
    public class Category
    {
        [Key]
        [Required]
        public int CategoryId { get; set; }

        [Required]
        public string CategoryName { get; set; }

        [Required]
        public string ImageUrl { get; set; }
        public bool IsDeleted { get; set; }
    }
}