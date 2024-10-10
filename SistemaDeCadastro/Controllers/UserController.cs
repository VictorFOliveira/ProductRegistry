using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SistemaDeCadastro.Models.ModelsDTO;
using SistemaDeCadastro.Repositories.Interfaces;
using Swashbuckle.AspNetCore.Annotations;

namespace SistemaDeCadastro.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize(Roles = "Admin")]
    public class UserController : ControllerBase
    {
        private readonly IUser _userRepository;

        public UserController(IUser userRepository)
        {
            _userRepository = userRepository;
        }

        /// <summary>
        /// Cria um novo usuário
        /// </summary>
        /// <param name="requestUserDTO">Os dados do usuário a ser criado.</param>
        /// <returns>O usuário criado.</returns>
        [HttpPost]
        [SwaggerOperation(Summary = "Cria um novo usuário", Description = "Adiciona um novo usuário ao sistema.")]
        [SwaggerResponse(201, "Usuário criado com sucesso.")]
        [SwaggerResponse(400, "Solicitação inválida.")]
        public async Task<IActionResult> CreateUser([FromBody] RequestUserDTO requestUserDTO)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var createUser = await _userRepository.Criar(requestUserDTO);
            return CreatedAtAction(nameof(GetById), new { id = createUser.Id }, createUser);
        }

        /// <summary>
        /// Obtém um usuário pelo ID
        /// </summary>
        /// <param name="id">O ID do usuário.</param>
        /// <returns>O usuário correspondente ao ID.</returns>
        [HttpGet("{id}")]
        [SwaggerOperation(Summary = "Obtém um usuário pelo ID", Description = "Retorna os detalhes de um usuário específico.")]
        [SwaggerResponse(200, "Usuário retornado com sucesso.")]
        [SwaggerResponse(404, "Usuário não encontrado.")]
        public async Task<IActionResult> GetById(int id)
        {
            var userId = await _userRepository.GetById(id);
            if (userId == null)
            {
                return NotFound();
            }

            return Ok(userId);
        }

        /// <summary>
        /// Remove o acesso de um usuário regular
        /// </summary>
        /// <param name="id">O ID do usuário cujo acesso será removido.</param>
        /// <returns>O usuário com acesso removido.</returns>
        [HttpPatch("blockAcess/{id}")]
        [SwaggerOperation(Summary = "Remove o acesso de um usuário", Description = "Remove o acesso de um usuário regular.")]
        [SwaggerResponse(200, "Acesso removido com sucesso.")]
        [SwaggerResponse(404, "Usuário não encontrado.")]
        public async Task<IActionResult> RemoverAcessoRegular(int id)
        {
            var userId = await _userRepository.RemoverAcessoRegular(id);
            if (userId == null)
            {
                return NotFound();
            }
            return Ok(userId);
        }

        /// <summary>
        /// Permite o acesso a um usuário regular
        /// </summary>
        /// <param name="id">O ID do usuário cujo acesso será permitido.</param>
        /// <returns>O usuário com acesso permitido.</returns>
        [HttpPatch("permitirAcess/{id}")]
        [SwaggerOperation(Summary = "Permite o acesso de um usuário", Description = "Permite o acesso a um usuário regular.")]
        [SwaggerResponse(200, "Acesso permitido com sucesso.")]
        [SwaggerResponse(404, "Usuário não encontrado.")]
        public async Task<IActionResult> PermitirAcessoRegular(int id)
        {
            var userPermitidoId = await _userRepository.PermitirAcessoRegular(id);
            if (userPermitidoId == null)
            {
                return NotFound();
            }
            return Ok(userPermitidoId);
        }
    }
}
