using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using UserSubscriptionWebApi.Exceptions;
using UserSubscriptionWebApi.IServices;
using UserSubscriptionWebApi.Models.DTOs;

namespace UserSubscriptionWebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SubscriptionTypeController : ControllerBase
    {
        private readonly ISubscriptionType _subscriptionType;
        private readonly ILogger<SubscriptionTypeController> _logger;

        public SubscriptionTypeController(ISubscriptionType subscriptionType, ILogger<SubscriptionTypeController> logger)
        {
            _subscriptionType = subscriptionType;
            _logger = logger;
        }

        [HttpGet]
        [Authorize(Roles = "ADMIN,USER")]
        public async Task<IActionResult> GetALL()
        {
            _logger.LogDebug("Getting all subscription types");
            var result = await _subscriptionType.GetALL();
            return Ok(result != null ? result : new int[0]);

        }


        [HttpPost]
        [Authorize(Roles = "ADMIN")]
        public async Task<IActionResult> Create([FromBody] SubscriptionTypeRequestDTO requestDTO)
        {
            if (ModelState.IsValid)
            {
                _logger.LogDebug("Creating new subscription type");
                var result = await _subscriptionType.Create(requestDTO);
                if (result == null)
                {
                    throw new BadRequestException("Server Error");
                }
                _logger.LogDebug($"New subscription type {requestDTO.Name} created successfully");
                return new JsonResult(result) { StatusCode = 201 };
            }
            else
                throw new BadRequestException("Server Error");
        }
    }
}
