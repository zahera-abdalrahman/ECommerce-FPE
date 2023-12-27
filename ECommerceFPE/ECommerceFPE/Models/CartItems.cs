using System.ComponentModel.DataAnnotations;

namespace ECommerceFPE.Models
{
    public class CartItems
    {
        [Key]
        public int CartItemsId { get; set; }

        [Required]
        public int Quantity { get; set; }


        [Required]
        public int CartId { get; set; }
        public Cart Cart { get; set; }

        [Required]
        public int ProductId { get; set; }
        public ProductCatalog ProductCatalog { get; set; }
    }
}
