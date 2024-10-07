using SistemaDeCadastro.Models.ModelsDTO;
using SistemaDeCadastro.Models;

namespace SistemaDeCadastro.Repositories.Interfaces
{
    public interface IUser
    {
        Task<ResponseUserDTO> Criar(RequestUserDTO requestUserDTO);
        Task<ResponseUserDTO> GetById(int id);

    }
}
