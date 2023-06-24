using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using UserSubscriptionWebApi.IServices;
using UserSubscriptionWebApi.Models.DTOs;

namespace UserSubscriptionWebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IProductService _productService;

        public ProductController(IProductService productService)
        {
            _productService = productService;
        }

        [HttpGet]
        [Route("get")]
        [Authorize(Roles = "ADMIN,USER")]
        public async Task<IActionResult> GetALL()
        {
            var result = await _productService.GetALL();
            if (result == null)
            {
                return BadRequest("Server Error");
            }
            return Ok(result);

        }

        [HttpPost]
        [Route("create")]
        [Authorize(Roles = "ADMIN")]
        public async Task<IActionResult> Create([FromBody] ProductRequestDTO requestDTO)
        {
            if (ModelState.IsValid)
            {
                var result = await _productService.Create(requestDTO);
                if (result == null)
                {
                    return BadRequest("Server Error");
                }
                return new JsonResult(result) { StatusCode = 201 };

            }
            else
                return BadRequest();

        }
    }
}
