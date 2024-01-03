using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ECommerceFPE.Models
{
    public class CreditCard
    {
        [Key]
        public int CreditCardID { get; set; }

        [Required]
        public string CardNumber { get; set; }


        [Required]
        public string password { get; set; }

       
        //public string UserId { get; set; }

        //public ApplicationUser ApplicationUser { get; set; }



    }
}
