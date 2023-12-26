using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace ECommerceFPE.Models.ViewModels
{
    public class RegisterViewModel
    {

        [Required(ErrorMessage = "Enter Email")]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        [Required(ErrorMessage = "Enter Your First Name")]
        public string FirstName { get; set; }
        [Required(ErrorMessage = "Enter Your Last Name")]
        public string LastName { get; set; }
        [Required(ErrorMessage = "Enter Your Country")]
        public string Country { get; set; }
        [Required(ErrorMessage = "Enter Your Gender")]
        public string Gender { get; set; }

        [Required(ErrorMessage = "Enter Password")]
        [DataType(DataType.Password)]
        public string Password { get; set; }
        [Required(ErrorMessage = "Enter Confirm Password")]
        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessage = "Email Not Match")]
        public string ConfirmPassword { get; set; }

    }
}
