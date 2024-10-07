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

        public UserRepository(AppDbContext context)
        {
            _context = context;
            _mapper = new UserMapper();
        }

        public async Task<ResponseUserDTO> Criar(RequestUserDTO requestUserDTO)
        {
            ValidationUser.Validate(requestUserDTO);

            UserModel userCreateModel = _mapper.MapToModel(requestUserDTO);

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
    }
}
