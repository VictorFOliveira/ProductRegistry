using Microsoft.AspNetCore.Mvc;
using SistemaDeCadastro.Models.ModelsDTO;
using SistemaDeCadastro.Repositories.Interfaces;

namespace SistemaDeCadastro.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductController : ControllerBase
    {
        private readonly IProduct _productRepository;

        public ProductController(IProduct productRepository)
        {
            _productRepository = productRepository;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllProducts()
        {
            var products = await _productRepository.GetAll();
            return Ok(products);
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var productId = await _productRepository.GetById(id);
            return Ok(productId);
        }

        [HttpPost]
        public async Task<IActionResult> CreateProduct([FromBody] RequestProductDTO requestProductDTO)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var createProduct = await _productRepository.Criar(requestProductDTO);
            return CreatedAtAction(nameof(CreateProduct), createProduct);

        }
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateProduct(int id, [FromBody] RequestProductDTO requestProductDTO)
        {
            if (!ModelState.IsValid)
            {

                return BadRequest(ModelState);
            }

            var updateProduct = await _productRepository.Editar(id, requestProductDTO);
            return Ok(updateProduct);

        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProduct(int id)
        {
            var productExisting = await _productRepository.Delete(id);
            return NoContent();

        }


    }
}
