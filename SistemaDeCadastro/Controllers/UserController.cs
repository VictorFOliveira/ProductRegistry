using Microsoft.AspNetCore.Mvc;
using SistemaDeCadastro.Models.ModelsDTO;
using SistemaDeCadastro.Repositories.Interfaces;

namespace SistemaDeCadastro.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserController : ControllerBase
    {
        private readonly IUser _userRepository;

        public UserController(IUser userRepository)
        {
            _userRepository = userRepository;
        }


        [HttpPost]
        public async Task<IActionResult> CreateUser([FromBody] RequestUserDTO requestUserDTO)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var createUser = await _userRepository.Criar(requestUserDTO);
            return CreatedAtAction(nameof(GetById), new { id = createUser.Id }, createUser);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var UserId = await _userRepository.GetById(id);
            if (UserId == null)
            {
                return NotFound();
            }

            return Ok(UserId);
        }
    }
}
