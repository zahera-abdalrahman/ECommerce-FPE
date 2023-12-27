using System.ComponentModel.DataAnnotations;

namespace ECommerceFPE.Models
{
    public class Product
    {

        public int ProductId { get; set; }
        [Display(Name = "Product Name")]
        [Required(ErrorMessage = "Product Name")]
        public string ProductName { get; set; }
        [Display(Name = "Product Price")]
        [Required(ErrorMessage = "Product Price")]
        public double ProductPrice { get; set; }
        public int ProductInStock { get; set; }
        [Display(Name = "Product Img")]
        [DataType(DataType.ImageUrl)]
        public string ProductImg { get; set; }
        public List<Order> Orders { get; set; }
    }
}
