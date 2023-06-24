using System.ComponentModel.DataAnnotations;

namespace UserSubscriptionWebApi.Models.DTOs
{
    public class SubscriptionType
    {
        [Key]
        public int SubscriptionTypeId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Price { get; set; }
        public ICollection<Subscription> Subscriptions { get; set; }

    }
}
