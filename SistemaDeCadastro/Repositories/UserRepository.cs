using Microsoft.EntityFrameworkCore;
using Serilog;
using SistemaDeCadastro.Data;
using SistemaDeCadastro.Mapper;
using SistemaDeCadastro.Models;
using SistemaDeCadastro.Models.ModelsDTO;
using SistemaDeCadastro.Repositories.Interfaces;
using SistemaDeCadastro.Validation;

namespace SistemaDeCadastro.Repositories
{
    public class UserRepository : IUser
    {

        private readonly AppDbContext _context;
        private readonly UserMapper _mapper;
        private readonly IHttpContextAccessor _httpContexAcessor;

        public UserRepository(AppDbContext context, IHttpContextAccessor httpContexAcessor)
        {
            _context = context;
            _mapper = new UserMapper();
            _httpContexAcessor = httpContexAcessor;
        }

        public async Task<UserModel> RemoverAcessoRegular(int id)
        {
            UserModel user = await _context.Users.FirstOrDefaultAsync(x => x.Id == id);
            
            if (user == null) 
            {
                Log.Error("Usuário não existe");
            }

            var alteracaoPor = _httpContexAcessor.HttpContext?.User.Identity.Name;

            if(user.acesso == Enum.Acesso.Regular)
            {
                user.acesso = Enum.Acesso.SemAcesso;
                user.DateAlteration = DateTime.Now;
                user.UltimaAlteracaoPor = alteracaoPor;


                await _context.SaveChangesAsync();
            }

            return user;
        }


        public async Task<ResponseUserDTO> Criar(RequestUserDTO requestUserDTO)
        {
            ValidationUser.Validate(requestUserDTO);

            UserModel userCreateModel = _mapper.MapToModel(requestUserDTO);

            ConfigureUser(userCreateModel);
            
            await _context.Users.AddAsync(userCreateModel);

            await _context.SaveChangesAsync();

            ResponseUserDTO responseUserDTO = _mapper.MapToResponse(userCreateModel);

            return responseUserDTO;

        }

        public async Task<ResponseUserDTO> GetById(int id)
        {

            UserModel userModel = await _context.Users.FirstOrDefaultAsync(x => x.Id == id);

            if (userModel == null)
            {
                Log.Error("Usuário nulo", id);
                throw new KeyNotFoundException("usuário não existe");
            }


            ResponseUserDTO responseUserDTO = _mapper.MapToResponse(userModel);
            return responseUserDTO;

        }

        public async Task<UserModel> GetUserByUserNameAsync(string userName)
        {

            return await _context.Users.FirstOrDefaultAsync(u => u.UserName == userName);
        }

        private string GenerateRandomMatricula()
        {
            Random random = new Random();

            int minValue = 100000;
            int maxValue = 999999;

            if (minValue > maxValue)
                throw new ArgumentOutOfRangeException("minValue", "O valor mínimo não pode ser maior ou igual ao valor máximo.");

            int matricula = random.Next(minValue, maxValue + 1);

            return matricula.ToString();
        }

        private void ConfigureUser(UserModel user)
        {
            var alteracaoPor = _httpContexAcessor.HttpContext?.User.Identity.Name;

            user.PasswordHash = BCrypt.Net.BCrypt.HashPassword(user.PasswordHash);
            user.Matricula = GenerateRandomMatricula();
            user.UltimaAlteracaoPor = alteracaoPor;
        }

        public async Task<UserModel> PermitirAcessoRegular(int id)
        {
            var usuarioPermitido = await _context.Users.FirstOrDefaultAsync(x=>x.Id == id);

            var alteracaoPor = _httpContexAcessor.HttpContext?.User.Identity.Name;

            if(usuarioPermitido.acesso == Enum.Acesso.SemAcesso)
            {
                usuarioPermitido.acesso = Enum.Acesso.Regular;
                usuarioPermitido.UltimaAlteracaoPor = alteracaoPor;
                usuarioPermitido.DateAlteration = DateTime.Now;

                await _context.SaveChangesAsync();
            }

            return usuarioPermitido;

        }
    }
}
