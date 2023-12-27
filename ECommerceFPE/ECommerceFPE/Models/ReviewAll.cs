namespace ECommerceFPE.Models
{
    public class ReviewAll
    {
        public int ReviewId { get; set; }

        // Foreign Key for User
        public int CustomerId { get; set; }
        public Customer Customer { get; set; }

        public string Comment { get; set; }
        public int Rating { get; set; }
        public DateTime ReviewDate { get; set; }
    }
}
