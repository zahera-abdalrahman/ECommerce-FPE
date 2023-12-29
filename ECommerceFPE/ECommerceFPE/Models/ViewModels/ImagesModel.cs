using System.ComponentModel.DataAnnotations;

namespace ECommerceFPE.Models.ViewModels
{
    public class ImagesModel
    {
        [Key]
        public int ImageId { get; set; }

        [Required]
        public string MainImageUrl { get; set; }


        [Required]
        public string MainImageUrl1 { get; set; }

        [Required]
        public string MainImageUrl2 { get; set; }

        [Required]
        public string MainImageUrl3 { get; set; }

        [Required]
        public int ImageOrder { get; set; }

        public IFormFile MainImageUrlFile { get; set; }

        public IFormFile MainImageUrl1File { get; set; }
        public IFormFile MainImageUrl2File { get; set; }

        public IFormFile MainImageUrl3File { get; set; }


        // Foreign Keys
        [Required]
        public int ProductId { get; set; }
        public Product ProductCatalog { get; set; }
    }
}
