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

        private readonly ILogger<AuthenticationController> _logger;

        public AuthenticationController(IAuthService authService, ILogger<AuthenticationController> logger)
        {
            _authService = authService;
            _logger = logger;
        }

        [HttpPost]
        [Route("Register")]
        public async Task<IActionResult> Register([FromBody] UserRegistrationRequestDTO requestDTO)
        {
            if (ModelState.IsValid)
            {
                _logger.LogDebug("new user is registring");

                var result = await _authService.Register(requestDTO);
                if (!result.Result)
                {
                    return result.Errors[0] == "Email already exists" ? throw new ObjectAlreadyExistsException("Email already exists") : throw new BadRequestException("Server Error");
                }

                _logger.LogDebug($"new user{requestDTO.Username} registered successfully");

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
                _logger.LogDebug($"{requestDTO.Email} is Logging in");
                var result = await _authService.Login(requestDTO);
                if (!result.Result)
                {
                    throw new BadRequestException(result.Errors[0]);
                }
                _logger.LogDebug($"{requestDTO.Email} is logged in successfully");
                return Ok(result);
            }
            throw new BadRequestException("Inavlid payload");
        }

    }
}
