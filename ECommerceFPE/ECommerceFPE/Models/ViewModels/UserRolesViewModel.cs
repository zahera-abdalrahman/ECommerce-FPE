using System.Collections.Generic;

namespace ECommerceFPE.Models.ViewModels
{
    public class UserRolesViewModel
    {
        public string UserId { get; set; }

        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Country { get; set; }
        public string Gender { get; set; }
        public IEnumerable<string> Roles { get; set; }
    }
}
