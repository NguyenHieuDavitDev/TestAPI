using Microsoft.AspNetCore.Mvc;
using ProductAPI.Modules.Product.DTOs;
using ProductAPI.Modules.Product.Services.Interfaces;

namespace ProductAPI.Modules.Product.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly IProductService _service;

        public ProductsController(
            IProductService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var result = await _service.GetAllAsync();

            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var result = await _service.GetByIdAsync(id);

            if (result == null)
            {
                return NotFound();
            }

            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> Create(
            [FromForm] CreateProductDto dto)
        {
            var result = await _service.CreateAsync(dto);

            return Ok(result);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(
            int id,
            [FromForm] UpdateProductDto dto)
        {
            var result =
                await _service.UpdateAsync(id, dto);

            if (result == null)
            {
                return NotFound();
            }

            return Ok(result);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await _service.DeleteAsync(id);

            if (!result)
            {
                return NotFound();
            }

            return Ok(new
            {
                message = "Delete success"
            });
        }
    }
}