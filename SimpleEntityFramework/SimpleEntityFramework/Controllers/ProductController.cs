using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SimpleEntityFramework.Data.Repositories.Interfaces;
using SimpleEntityFramework.Entities;

namespace SimpleEntityFramework.Controllers
{
    [Route("api/products")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IProductRepository _productRepository;
        public ProductController(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var result = await _productRepository.GetAll();

            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var result = await _productRepository.GetById(id);

            if (result == null)
                return NotFound();

            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> Create(Product product)
        {
            var result = await _productRepository.Create(product);

            return Ok(result);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Create(int id, Product product)
        {
            var result = await _productRepository.Update(product, id);

            return Ok(result);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await _productRepository.Delete(id);

            return Ok(result);
        }

    }
}
