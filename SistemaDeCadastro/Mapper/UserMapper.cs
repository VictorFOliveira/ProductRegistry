using Serilog;
using SistemaDeCadastro.Models.ModelsDTO;
using SistemaDeCadastro.Models;

namespace SistemaDeCadastro.Mapper
{
    public class UserMapper
    {
        public UserModel MapToModel(RequestUserDTO requestUserDTO)
        {
            if (requestUserDTO == null)
            {
                Log.Error("A requisição não pode ser vazia");
                throw new ArgumentNullException(nameof(requestUserDTO));
            }

            return new UserModel
            {
               
                UserName = requestUserDTO.UserName,
                PasswordHash = requestUserDTO.PasswordHash,
                acesso = requestUserDTO.Acesso
            };
        }


        public ResponseUserDTO MapToResponse(UserModel userModel)
        {

            if (userModel == null)
            {
                Log.Error("A requisição não pode ser vazia");
                throw new ArgumentNullException(nameof(userModel));

            }

            return new ResponseUserDTO
            {

                Id = userModel.Id,
                UserName = userModel.UserName,
                acesso = userModel.acesso,
            };
        }

    }
}

