using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using UserSubscriptionWebApi.IServices;
using UserSubscriptionWebApi.Models.DTOs;

namespace UserSubscriptionWebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SubscriptionController : ControllerBase
    {
        private readonly ISubscriptionService _subscription;

        public SubscriptionController(ISubscriptionService subscription)
        {
            _subscription = subscription;
        }


        [HttpGet("/{userId}")]
        [Authorize(Roles = "ADMIN,USER")]
        public async Task<IActionResult> GetSubscriptionByUser(string userId)
        {
            var result = await _subscription.GetSubscriptionsByUser(userId);
            if (result == null)
            {
                return BadRequest("Server Error");
            }
            return Ok(result);

        }


        [HttpPost]
        [Authorize(Roles = "ADMIN,USER")]
        public async Task<IActionResult> Create([FromBody] SubscriptionRequestDTO requestDTO)
        {
            if (ModelState.IsValid)
            {
                var result = await _subscription.Create(requestDTO);
                if (result == null)
                {
                    return BadRequest("Server Error");
                }
                return new JsonResult(result) { StatusCode = 201 };

            }
            else
                return BadRequest();

        }

        [HttpGet("days/{subscriptionId}")]
        [Authorize(Roles = "ADMIN,USER")]
        public async Task<IActionResult> GetSubscriptionRemainingDays(int subscriptionId)
        {
            var result = await _subscription.GeTSubscriptionRemainingDays(subscriptionId);
            if (result == 0)
            {
                return Ok("Your subscription is already not active");
            }
            return Ok(result);

        }
    }
}
