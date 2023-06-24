using System.ComponentModel.DataAnnotations;

namespace UserSubscriptionWebApi.Models.DTOs
{
    public class UserRegistrationRequestDTO
    {
        [Required]
        public string Username { get; set; }
        [Required]
        public string Email { get; set; }
        [Required]
        public string Password { get; set; }
    }
}
