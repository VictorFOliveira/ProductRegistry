using SistemaDeCadastro.Models;
using SistemaDeCadastro.Models.ModelsDTO;

namespace SistemaDeCadastro.Repositories.Interfaces
{
    public interface IProduct
    {
        Task<ProductModel> Criar(RequestProductDTO requestProductDTO);
        Task<ProductModel> GetById(int id);
        Task<IEnumerable<ProductModel>> GetAll();
        Task<ProductModel> Editar(int id, RequestProductDTO requestProductDTO);
        Task<bool> Delete(int id);

    }
}
