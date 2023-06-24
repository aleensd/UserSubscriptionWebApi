using Microsoft.AspNetCore.Identity;
using UserSubscriptionWebApi.Models;

namespace UserSubscriptionWebApi.Data
{
    public class ApplicationUser : IdentityUser
    {
        // Additional properties or attributes
        public ICollection<Subscription> Subscriptions { get; set; }
    }

}
