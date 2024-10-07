using Microsoft.AspNetCore.Mvc;
using SistemaDeCadastro.Models.ModelsDTO;
using SistemaDeCadastro.Repositories.Interfaces;
using Swashbuckle.AspNetCore.Annotations;

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

        /// <summary>
        /// Obtém todos os produtos
        /// </summary>
        /// <returns>Uma lista de produtos</returns>
        [HttpGet]
        [SwaggerOperation(Summary = "Obtém todos os produtos", Description = "Retorna uma lista de produtos cadastrados.")]
        [SwaggerResponse(200, "Lista de produtos retornada com sucesso.")]
        public async Task<IActionResult> GetAllProducts()
        {
            var products = await _productRepository.GetAll();
            return Ok(products);
        }

        /// <summary>
        /// Obtém um produto pelo ID
        /// </summary>
        /// <param name="id">O ID do produto.</param>
        /// <returns>O produto correspondente ao ID.</returns>
        [HttpGet("{id}")]
        [SwaggerOperation(Summary = "Obtém um produto pelo ID", Description = "Retorna os detalhes de um produto específico.")]
        [SwaggerResponse(200, "Produto retornado com sucesso.")]
        [SwaggerResponse(404, "Produto não encontrado.")]
        public async Task<IActionResult> GetById(int id)
        {
            var productId = await _productRepository.GetById(id);
            if (productId == null)
            {
                return NotFound();
            }
            return Ok(productId);
        }

        /// <summary>
        /// Criar um novo produto
        /// </summary>
        /// <param name="requestProductDTO">Os dados do produto a ser criado.</param>
        /// <returns>O produto criado.</returns>
        [HttpPost]
        [SwaggerOperation(Summary = "Cria um novo produto", Description = "Adiciona um novo produto ao sistema.")]
        [SwaggerResponse(201, "Produto criado com sucesso.")]
        [SwaggerResponse(400, "Solicitação inválida.")]
        public async Task<IActionResult> CreateProduct([FromBody] RequestProductDTO requestProductDTO)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var createProduct = await _productRepository.Criar(requestProductDTO);
            return CreatedAtAction(nameof(GetById), new { id = createProduct.Id }, createProduct); // Corrigido para retornar o novo produto
        }

        /// <summary>
        /// Atualizar um produto existente
        /// </summary>
        /// <param name="id">O ID do produto a ser atualizado.</param>
        /// <param name="requestProductDTO">Dados atualizados do produto.</param>
        /// <returns>O produto atualizado.</returns>
        [HttpPut("{id}")]
        [SwaggerOperation(Summary = "Atualiza um produto existente", Description = "Atualiza os detalhes de um produto já cadastrado.")]
        [SwaggerResponse(200, "Produto atualizado com sucesso.")]
        [SwaggerResponse(400, "Solicitação inválida.")]
        [SwaggerResponse(404, "Produto não encontrado.")]
        public async Task<IActionResult> UpdateProduct(int id, [FromBody] RequestProductDTO requestProductDTO)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var updateProduct = await _productRepository.Editar(id, requestProductDTO);
            if (updateProduct == null)
            {
                return NotFound();
            }
            return Ok(updateProduct);
        }

        /// <summary>
        /// Deletar um produto
        /// </summary>
        /// <param name="id">O ID do produto a ser deletado.</param>
        /// <returns>Uma resposta indicando a exclusão.</returns>
        [HttpDelete("{id}")]
        [SwaggerOperation(Summary = "Deleta um produto", Description = "Remove um produto do sistema.")]
        [SwaggerResponse(204, "Produto deletado com sucesso.")]
        [SwaggerResponse(404, "Produto não encontrado.")]
        public async Task<IActionResult> DeleteProduct(int id)
        {
            var productExisting = await _productRepository.Delete(id);
            if (!productExisting)
            {
                return NotFound();
            }
            return NoContent();
        }
    }
}
