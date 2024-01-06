using System.ComponentModel.DataAnnotations;

namespace ECommerceFPE.Models.ViewModels
{
    public class ProfileViewModel
    {
        public ApplicationUser User { get; set; }
        public List<Order> Orders { get; set; }

        [Display(Name = "Edit Details")]
        public EditViewModel EditModel { get; set; }
    }
}
