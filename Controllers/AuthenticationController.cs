using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using UserSubscriptionWebApi.Configurations;
using UserSubscriptionWebApi.Exceptions;
using UserSubscriptionWebApi.IServices;
using UserSubscriptionWebApi.Models;
using UserSubscriptionWebApi.Models.DTOs;

namespace UserSubscriptionWebApi.Controllers
{
    [Route("api/[controller]")] // api/authentication
    [ApiController]
    public class AuthenticationController : ControllerBase
    {
        private readonly IAuthService _authService;

        public AuthenticationController(IAuthService authService)
        {
            _authService = authService;
        }

        [HttpPost]
        [Route("Register")]
        public async Task<IActionResult> Register([FromBody] UserRegistrationRequestDTO requestDTO)
        {
            if (ModelState.IsValid)
            {
                var result = await _authService.Register(requestDTO);

                if (!result.Result)
                {
                    return result.Errors[0] == "Email already exists" ? throw new ObjectAlreadyExistsException("Email already exists") : throw new BadRequestException("Server Error");
                }

                return Ok(result);

            }
            else
                throw new BadRequestException("Server Error");

        }

        [HttpPost]
        [Route("Login")]
        public async Task<IActionResult> Login([FromBody] UserLoginRequestDTO requestDTO)
        {
            if (ModelState.IsValid)
            {
                var result = await _authService.Login(requestDTO);
                if (!result.Result)
                {
                    throw new BadRequestException(result.Errors[0]);
                }

                return Ok(result);
            }
            throw new BadRequestException("Inavlid payload");
        }

    }
}
