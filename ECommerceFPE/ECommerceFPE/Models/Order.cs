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

<<<<<<< Updated upstream
=======
      
        public int CartId { get; set; }
        public  Cart Cart { get; set; }

>>>>>>> Stashed changes
        // Foreign Keys
        [Required]
        public int CustomerId { get; set; }
        public Customer Customer { get; set; }
        public List<Product> Products { get; set; }

        // Collection Navigation Properties

<<<<<<< Updated upstream
=======
        public ApplicationUser ApplicationUser { get; set; }
        public bool IsDeleted { get; set; }
>>>>>>> Stashed changes
    }
}
