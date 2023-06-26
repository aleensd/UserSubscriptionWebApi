using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using UserSubscriptionWebApi.Exceptions;
using UserSubscriptionWebApi.IServices;
using UserSubscriptionWebApi.Models;
using UserSubscriptionWebApi.Models.DTOs;

namespace UserSubscriptionWebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IProductService _productService;

        private readonly ILogger<ProductController> _logger;

        public ProductController(IProductService productService, ILogger<ProductController> logger)
        {
            _productService = productService;
            _logger = logger;
        }

        [HttpGet]
        [Authorize(Roles = "ADMIN,USER")]
        public async Task<IActionResult> GetALL([FromQuery] int? page, [FromQuery] int? limit)
        {

            if (page <= 0 || limit <= 0)
            {
                throw new BadRequestException("Page cannot be 0 and limit should be greater than zero");
            }
            if (page == null && limit == null)
            {
                var all = await _productService.GetALL();
                return Ok(all);
            }

            _logger.LogDebug("Getting all Products");

            var result = await _productService.GetALLPaginated((int)page, (int)limit);

            return Ok(result != null ? result : new int[0]);

        }

        [HttpGet("product/{Id}")]
        [Authorize(Roles = "ADMIN,USER")]
        public async Task<IActionResult> GetById(int Id)
        {
            _logger.LogDebug($"Getting product by id {Id}");

            var result = await _productService.GetById(Id);
            if (result == null)
            {
                throw new NotFoundException("id is invalid");
            }
            return Ok(result);

        }

        [HttpPost]
        [Authorize(Roles = "USER")]
        public async Task<IActionResult> Create([FromBody] ProductRequestDTO requestDTO)
        {
            _logger.LogDebug($"Creating product {requestDTO}");

            if (ModelState.IsValid)
            {
                var result = await _productService.Create(requestDTO);
                return new JsonResult(result) { StatusCode = 201 };
            }
            else
                throw new BadRequestException("Server Error");

        }
    }
}
