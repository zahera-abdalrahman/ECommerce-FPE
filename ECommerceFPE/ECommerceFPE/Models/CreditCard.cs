namespace ECommerceFPE.Models
{
    public class CreditCard
    {
        public int CreditCardID { get; set; }

        public string CardNumber { get; set; }

        public DateTime ExpiryDate { get; set; }

        public int CustomerId { get; set; }

        public Customer Customer { get; set; }



    }
}
