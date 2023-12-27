using System.ComponentModel.DataAnnotations;

namespace ECommerceFPE.Models
{
    public class Order
    {
        [Key]
        [Required]
        public int OrderId { get; set; }

        [Required]
        public DateTime OrderDate { get; set; }

        [Required]
        public string TotalAmount { get; set; }

        [Required]
        public string OrderStatus { get; set; }

        // Foreign Keys
        [Required]
        public int CustomerId { get; set; }
        public Customer Customer { get; set; }
        public List<Product> Products { get; set; }

        // Collection Navigation Properties

    }
}
