using System.ComponentModel.DataAnnotations;

namespace ECommerceFPE.Models
{
    public class Category
    {
        [Key]
        public int CategoryId { get; set; }

        [Required]
        public string CategoryName { get; set; }

        [Required]
        public string ImageUrl { get; set; }

    }
}