using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ECommerceFPE.Models
{
    public class Payment
    {
  
        [Key]
        public int PaymentId { get; set; }

        //[ForeignKey("CreditCard")]
        //public int CreditCardID { get; set; }

        //public CreditCard CreditCard { get; set; }

        [Required]
        public decimal Amount { get; set; }


        [Required]
        [DataType(DataType.DateTime)]
        public DateTime TransactionDate { get; set; }

        public string UserId { get; set; }
        public ApplicationUser ApplicationUser { get; set; }

    }
}
