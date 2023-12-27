using System.ComponentModel.DataAnnotations;

namespace ECommerceFPE.Models
{
    public class Customer
    {
        [Key]
        [Required]
        public int CustomerId { get; set; }
        [Display(Name = "Customer Name")]
        [Required]
        public string CustomerName { get; set; }
        [Required]
        [StringLength(
            50,
            MinimumLength = 5,
            ErrorMessage = "Address must be between 5 and 50 characters"
        )]
        public string Address { get; set; }

        // Collections navigation properties
        public ICollection<Review> Reviews { get; set; }
        public ICollection<Order> Orders { get; set; }

        public ICollection<Payment> Payments { get; set; }

        public int CreditCardID { get; set; }

        public CreditCard CreditCard { get; set; }
    }
}