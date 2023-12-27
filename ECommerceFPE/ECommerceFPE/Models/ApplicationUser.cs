using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ECommerceFPE.Models
{
    public class ApplicationUser: IdentityUser
    {
        [Required(ErrorMessage = "Enter First Name")]
        [MaxLength(15, ErrorMessage = "Max 15 Char")]
        [MinLength(3, ErrorMessage = "Min 3 Char")]
        [Display(Name = "First Name")]
        public string FirstName { get; set; }



        [Required(ErrorMessage = "Enter Last Name")]
        [Display(Name = "Last Name")]
        [MaxLength(15, ErrorMessage = "Max 15 Char")]
        [MinLength(3, ErrorMessage = "Min 3 Char")]
        public string LastName { get; set; }


        [Display(Name = "Country")]
        [Required(ErrorMessage = "Enter Country")]
        public string Country { get; set; }

        [Display(Name = "Gender")]
        [Required(ErrorMessage = "Enter Gender")]
        public string Gender { get; set; }
       



    }
    
}
