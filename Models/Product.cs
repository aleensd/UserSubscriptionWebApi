using System.ComponentModel.DataAnnotations;

namespace UserSubscriptionWebApi.Models
{
    public class Product
    {
        [Key]
        public int ProductId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public bool IsDeleted { get; set; }
        public ICollection<Subscription> Subscriptions { get; set; }
    }
}
