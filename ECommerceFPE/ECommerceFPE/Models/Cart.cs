using System.ComponentModel.DataAnnotations;

namespace ECommerceFPE.Models
{
    public class Cart
    {
        [Key]
        public int CartId { get; set; }
        public int CustomerId { get; set; }

        public Customer Customer { get; set; }

        public ICollection<CartItems> CartItems { get; set; }

    }
}
