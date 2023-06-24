using System.ComponentModel.DataAnnotations;

namespace UserSubscriptionWebApi.Models.DTOs
{
    public class UserLoginRequestDTO
    {
        [Required]
        public string Email { get; set; }
        [Required]
        public string Password { get; set; }
    }
}
