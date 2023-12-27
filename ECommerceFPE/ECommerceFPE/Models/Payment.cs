using System.ComponentModel.DataAnnotations;

namespace ECommerceFPE.Models
{
    public class Payment
    {
        [Key]
        public int PaymentId { get; set; }


        public int CreditCardID { get; set; }

        public CreditCard CreditCard { get; set; }

    }
}
