using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ECommerceFPE.Models
{
    public class CartItems
    {
        [Key]
        public int CartItemsId { get; set; }

        [Required]
        public int Quantity { get; set; }


        [Required]
        [ForeignKey("Cart")]
        public int CartId { get; set; }
        public Cart Cart { get; set; }

        [Required]
        [ForeignKey("Product")]
        public int ProductId { get; set; }
        public Product ProductCatalog { get; set; }
    }
}
