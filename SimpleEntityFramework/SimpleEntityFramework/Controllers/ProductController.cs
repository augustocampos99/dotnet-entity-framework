using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SimpleEntityFramework.Data.Repositories.Interfaces;
using SimpleEntityFramework.Dtos;
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
        public async Task<IActionResult> Create(ProductRequestDto productDto)
        {
            var product = new Product()
            {
                Name = productDto.Name,
                Price = productDto.Price,
                Quantity = productDto.Quantity,
                CategoryId = productDto.CategoryId
            };

            var result = await _productRepository.Create(product);

            return Ok(result);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Create(int id, ProductRequestDto productDto)
        {
            var product = new Product()
            {
                Name = productDto.Name,
                Price = productDto.Price,
                Quantity = productDto.Quantity,
                CategoryId = productDto.CategoryId
            };

            var result = await _productRepository.Update(product, id);

            return Ok(result);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await _productRepository.Delete(id);

            return Ok(result);
        }

        [HttpGet("query")]
        public async Task<IActionResult> GetAllRawQuery()
        {
            var result = await _productRepository.GetAllRawQuery();

            return Ok(result);
        }

        [HttpGet("{id}/query")]
        public async Task<IActionResult> GetByIdRawQuery(int id)
        {
            var result = await _productRepository.GetByIdRawQuery(id);

            if (result == null)
                return NotFound();

            return Ok(result);
        }

        [HttpGet("linq")]
        public async Task<IActionResult> GetAllLinqQuery()
        {
            var result = await _productRepository.GetAllLinqQuery();

            return Ok(result);
        }

        [HttpGet("{id}/linq")]
        public async Task<IActionResult> GetByIdLinqQuery(int id)
        {
            var result = await _productRepository.GetByIdLinqQuery(id);

            if (result == null)
                return NotFound();

            return Ok(result);
        }

    }
}
