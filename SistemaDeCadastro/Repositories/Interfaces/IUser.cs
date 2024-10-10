using SistemaDeCadastro.Models.ModelsDTO;
using SistemaDeCadastro.Models;

namespace SistemaDeCadastro.Repositories.Interfaces
{
    public interface IUser
    {
        Task<ResponseUserDTO> Criar(RequestUserDTO requestUserDTO);
        Task<ResponseUserDTO> GetById(int id);
        Task<UserModel> GetUserByUserNameAsync(string userName);
        Task<UserModel> RemoverAcessoRegular(int id);
        Task<UserModel> PermitirAcessoRegular(int id);

    }
}
