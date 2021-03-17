using Microsoft.AspNetCore.Identity;

namespace MOM.IS4Host.Models
{
    // Add profile data for application users by adding properties to the ApplicationUser class
    public class ApplicationUser : IdentityUser
    {
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public string LastName { get; set; }
        public bool IsEnabled { get; set; }
    }
}
