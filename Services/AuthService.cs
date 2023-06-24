using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using UserSubscriptionWebApi.Configurations;
using UserSubscriptionWebApi.Data;
using UserSubscriptionWebApi.IServices;
using UserSubscriptionWebApi.Models;
using UserSubscriptionWebApi.Models.DTOs;

namespace UserSubscriptionWebApi.Services
{
    public class AuthService : IAuthService
    {
        private readonly UserManager<ApplicationUser> _userManager;

        private readonly IConfiguration _configuration;


        public AuthService(UserManager<ApplicationUser> userManager, IConfiguration configuration)
        {
            _userManager = userManager;
            _configuration = configuration;
        }

        public async Task<AuthResult> Login(UserLoginRequestDTO requestDTO)
        {
            //Check if user exists
            var existing_user = await _userManager.FindByEmailAsync(requestDTO.Email);
            if (existing_user == null)
            {
                return new AuthResult()
                {
                    Result = false,
                    Errors = new List<string>
                    {
                        "Invalid Payload"
                    }
                };

            }
            var isPasswordCorrect = await _userManager.CheckPasswordAsync(existing_user, requestDTO.Password);
            if (!isPasswordCorrect)
            {
                return new AuthResult()
                {
                    Result = false,
                    Errors = new List<string>
                    {
                           "Invalid Credentials"
                    }

                };

            }
            //Generate token
            var token = GenerateToken(existing_user, "USER");
            return new AuthResult()
            {
                Token = token,
                Result = true,
            };

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

            var new_user = new ApplicationUser()
            {
                UserName = requestDTO.Email,
                Email = requestDTO.Email,

            };

            var is_created = await _userManager.CreateAsync(new_user, requestDTO.Password);
            if (is_created.Succeeded)
            {
                await _userManager.AddToRoleAsync(new_user, "user");

                //Generate token
                var token = GenerateToken(new_user, "USER");

                return new AuthResult()
                {
                    Token = token,
                    Result = true,
                };
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

        private string GenerateToken(ApplicationUser user, string role)
        {
            var jwtTokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.UTF8.GetBytes(_configuration.GetSection("JwtConfig:Secret").Value);

            //Token descriptor
            var tokenDescriptor = new SecurityTokenDescriptor()
            {
                Subject = new ClaimsIdentity(new[]
                {
                    new Claim("Id", user.Id),
                    new Claim(JwtRegisteredClaimNames.Sub,user.Email),
                    new Claim(JwtRegisteredClaimNames.Email,user.Email),
                    new Claim(JwtRegisteredClaimNames.Jti,Guid.NewGuid().ToString()),
                    new Claim(JwtRegisteredClaimNames.Iat,DateTime.Now.ToUniversalTime().ToString()),
                    new Claim(ClaimTypes.Role,role),

                }),
                Expires = DateTime.Now.AddHours(1),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256)
            };

            var token = jwtTokenHandler.CreateToken(tokenDescriptor);
            var jwtToken = jwtTokenHandler.WriteToken(token);
            return jwtToken;
        }
    }

}
