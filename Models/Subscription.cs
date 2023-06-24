using System.ComponentModel.DataAnnotations;
using UserSubscriptionWebApi.Data;
using UserSubscriptionWebApi.Models.DTOs;

namespace UserSubscriptionWebApi.Models
{
    public class Subscription
    {
        [Key]
        public int SubscriptionId { get; set; }
        public string ApplicationUserId { get; set; }
        public int ProductId { get; set; }
        public int SubscriptionTypeId { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public bool IsActive { get; set; }

        public ApplicationUser ApplicationUser { get; set; }
        public Product Product { get; set; }
        public SubscriptionType SubscriptionType { get; set; }
    }
}
