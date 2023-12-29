using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ECommerceFPE.Models
{
    public class Payment
    {
        [Key]
        public int PaymentId { get; set; }

        [ForeignKey("CreditCard")]
        public int CreditCardID { get; set; }

        public CreditCard CreditCard { get; set; }

    }
}
