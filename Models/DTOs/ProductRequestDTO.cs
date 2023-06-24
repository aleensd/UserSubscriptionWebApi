using System.ComponentModel.DataAnnotations;

namespace UserSubscriptionWebApi.Models.DTOs
{
    public class ProductRequestDTO
    {
        [Required]
        public string Name { get; set; }
        [Required]
        [MaxLength(50)]
        public string Description { get; set; }
    }
}
