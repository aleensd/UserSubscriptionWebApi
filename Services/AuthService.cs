using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using UserSubscriptionWebApi.IServices;
using UserSubscriptionWebApi.Models;
using UserSubscriptionWebApi.Models.DTOs;

namespace UserSubscriptionWebApi.Services
{
    public class AuthService : IAuthService
    {
        private readonly UserManager<IdentityUser> _userManager;

        public AuthService(UserManager<IdentityUser> userManager)
        {
            _userManager = userManager;
        }

        public async Task<AuthResult> Register(UserRegistrationRequestDTO requestDTO)
        {
            var user = await _userManager.FindByEmailAsync(requestDTO.Email);

            if (user != null)
            {
                return new AuthResult()
                {
                    Result = false,
                    Errors = new List<string>
                        {
                            "Email already exists"
                        }

                };
            }

            var new_user = new IdentityUser()
            {
                UserName = requestDTO.Username,
                Email = requestDTO.Email,

            };

            var is_created = await _userManager.CreateAsync(new_user, requestDTO.Password);
            if (is_created.Succeeded)
            {
                var user_role = await _userManager.AddToRoleAsync(new_user, "user");
                return new AuthResult()
                {
                    Result = true,
                };
                //Generate token

            }

            return new AuthResult()
            {
                Result = false,
                Errors = new List<string>
                        {
                            "Server error"
                        }

            };
        }
    }

}
