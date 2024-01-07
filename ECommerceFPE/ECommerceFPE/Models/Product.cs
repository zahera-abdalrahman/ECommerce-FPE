using System.ComponentModel.DataAnnotations;

namespace ECommerceFPE.Models
{
    public class Product
    {

        public int ProductId { get; set; }
        [Display(Name = "Product Name")]
        [Required(ErrorMessage = "Product Name")]
        public string ProductName { get; set; }
<<<<<<< Updated upstream
        [Display(Name = "Product Price")]
        [Required(ErrorMessage = "Product Price")]
        public double ProductPrice { get; set; }
        public int ProductInStock { get; set; }
        [Display(Name = "Product Img")]
        [DataType(DataType.ImageUrl)]
        public string ProductImg { get; set; }
        public List<Order> Orders { get; set; }
=======

        [Required]
        public string Description { get; set; }

        [Required]
        public double Price { get; set; }

        [Required]
        public int QuantityInStock { get; set; }
        public int CategoryId { get; set; }
        public Category Category { get; set; }

        public int DiscountPercent { get; set; }

        [Required]
        public string ImageUrl { get; set; }
        public bool IsDeleted { get; set; }

>>>>>>> Stashed changes
    }
}
