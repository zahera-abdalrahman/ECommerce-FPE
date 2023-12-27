using System.ComponentModel.DataAnnotations;

namespace ECommerceFPE.Models
{
    public class OrderItems
    {

        public int OrderItemId { get; set; }

        [Required]
        public int Quantity { get; set; }

        [Required]
        public double Price { get; set; }

        [Required]
        public double TotalPrice { get; set; }

        // Foreign Keys
        [Required]
        public int ProductId { get; set; }
        public ProductCatalog ProductCatalog { get; set; }

        [Required]
        public int OrderId { get; set; }
        public Order Order { get; set; }
    }
}