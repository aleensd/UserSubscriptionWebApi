using Microsoft.AspNetCore.Identity;

namespace UserSubscriptionWebApi.Data
{
    public class ApplicationUser : IdentityUser
    {
        // Additional properties or attributes
        public bool isDeleted { get; set; }=false;
    }

}
