using System.ComponentModel.DataAnnotations;

namespace UserSubscriptionWebApi.Models.DTOs
{
    public class SubscriptionRequestDTO
    {
        [Required]
        public string ApplicationUserId { get; set; }
        [Required]
        public int ProductId { get; set; }
        [Required]
        public int SubscriptionTypeId { get; set; }
        
    }
}
