﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ECommerceFPE.Models.ViewModels
{
    public class EditRoleViewModel
    {
       
            public EditRoleViewModel()
            {
                Users = new List<string>();
            }

            public string RoleId { get; set; }

            [Required(ErrorMessage = "Role Name is required")]
            public string RoleName { get; set; }

            public List<string> Users { get; set; }
        
    }
}
