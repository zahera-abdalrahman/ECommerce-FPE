using System.ComponentModel.DataAnnotations;

namespace ECommerceFPE.Models.ViewModels
{
    public class CategoryModel
    {
        [Key]
        public int CategoryId { get; set; }

        [Required]
        public string CategoryName { get; set; }

        [Required]
        public string ImageUrl { get; set; }

        public IFormFile File { get; set; }
    }
}
