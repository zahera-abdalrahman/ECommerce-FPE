using System.ComponentModel.DataAnnotations;

namespace ECommerceFPE.Models
{
    public class Images
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

        // Foreign Keys
        [Required]
        public int ProductId { get; set; }
        public Product ProductCatalog { get; set; }
    }
}