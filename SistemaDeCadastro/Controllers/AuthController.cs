using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using SistemaDeCadastro.Models.ModelsDTO;
using SistemaDeCadastro.Repositories.Interfaces;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace SistemaDeCadastro.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        private readonly IUser _userRepository;


        public AuthController(IConfiguration configuration, IUser userRepository)
        {

            _configuration = configuration;
            _userRepository = userRepository;
        }


        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] RequestLoginDTO requestLoginDTO)
        {
            var user = await _userRepository.GetUserByUserNameAsync(requestLoginDTO.UserName);
            if (user == null || !VerifyPassword(requestLoginDTO.Password, user.PasswordHash))
            {
                return Unauthorized();
            }


            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]);

            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, user.UserName),
                new Claim(ClaimTypes.Role, user.acesso.ToString())
            };

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.UtcNow.AddHours(1),
                Issuer = _configuration["Jwt:Issuer"],
                Audience = _configuration["Jwt:Audience"],
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };

            

            var token = tokenHandler.CreateToken(tokenDescriptor);
            return Ok(new { Token = tokenHandler.WriteToken(token) });
        }

        private bool VerifyPassword(string inputPassword, string storedPasswordHash)
        {
            return BCrypt.Net.BCrypt.Verify(inputPassword, storedPasswordHash);
        }

    }
}
