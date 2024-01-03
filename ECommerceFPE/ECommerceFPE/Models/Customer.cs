using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ECommerceFPE.Models
{
    public class Customer
    {
        [Key]
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


        [Required]
        [ForeignKey("CreditCard")]
        public int CreditCardID { get; set; }

        public CreditCard CreditCard { get; set; }

        [Required]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }
    }
}