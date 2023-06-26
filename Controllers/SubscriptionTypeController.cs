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

        public SubscriptionTypeController(ISubscriptionType subscriptionType)
        {
            _subscriptionType = subscriptionType;
        }

        [HttpGet]
        [Authorize(Roles = "ADMIN,USER")]
        public async Task<IActionResult> GetALL()
        {
            var result = await _subscriptionType.GetALL();
            return Ok(result != null ? result : new int[0]);

        }


        [HttpPost]
        [Authorize(Roles = "ADMIN")]
        public async Task<IActionResult> Create([FromBody] SubscriptionTypeRequestDTO requestDTO)
        {
            if (ModelState.IsValid)
            {
                var result = await _subscriptionType.Create(requestDTO);
                if (result == null)
                {
                    throw new BadRequestException("Server Error");
                }
                return new JsonResult(result) { StatusCode = 201 };

            }
            else
                throw new BadRequestException("Server Error");

        }
    }
}
