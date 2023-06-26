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
    public class SubscriptionController : ControllerBase
    {
        private readonly ISubscriptionService _subscription;
        private readonly ILogger<SubscriptionController> _logger;
        public SubscriptionController(ISubscriptionService subscription, ILogger<SubscriptionController> logger)
        {
            _subscription = subscription;
            _logger = logger;
        }


        [HttpGet("/{userId}")]
        [Authorize(Roles = "ADMIN,USER")]
        public async Task<IActionResult> GetSubscriptionByUser(string userId)
        {
            _logger.LogDebug($"Getting subscription details for {userId}");

            var result = await _subscription.GetSubscriptionsByUser(userId);
            if (result == null)
            {
                throw new BadRequestException("Server Error");
            }
            return Ok(result);

        }

        [HttpPost]
        [Authorize(Roles = "ADMIN,USER")]
        public async Task<IActionResult> Create([FromBody] SubscriptionRequestDTO requestDTO)
        {
            if (ModelState.IsValid)
            {
                _logger.LogDebug($"User {requestDTO.ApplicationUserId} subscriping to product {requestDTO.ProductId}");
                var result = await _subscription.Create(requestDTO);
                if (result == null)
                {
                    throw new BadRequestException("Server Error");
                }
                _logger.LogDebug($"User {requestDTO.ApplicationUserId} subscribed to product {requestDTO.ProductId} successfully");
                return new JsonResult(result) { StatusCode = 201 };

            }
            else
                throw new BadRequestException("Server Error");

        }

        [HttpGet("days/{subscriptionId}")]
        [Authorize(Roles = "ADMIN,USER")]
        public async Task<IActionResult> GetSubscriptionRemainingDays(int subscriptionId)
        {
            _logger.LogDebug($"Getting remaining subscription days for {subscriptionId}");
            var result = await _subscription.GeTSubscriptionRemainingDays(subscriptionId);
            if (result == 0)
            {
                return Ok("Your subscription is already not active");
            }
            return Ok(result);

        }
    }
}
