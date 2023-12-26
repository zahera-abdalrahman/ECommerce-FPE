using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ECommerceFPE.Models.ViewModels
{
    public class CreateRoleViewModel
    {
        [Required(ErrorMessage = "Enter Role Name")]       
        public string RoleName { get; set; }
    }
}
