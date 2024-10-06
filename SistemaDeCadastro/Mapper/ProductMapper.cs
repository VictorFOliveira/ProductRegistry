using Microsoft.EntityFrameworkCore.Metadata.Conventions;
using Serilog;
using SistemaDeCadastro.Models;
using SistemaDeCadastro.Models.ModelsDTO;

namespace SistemaDeCadastro.Mapper
{
    public class ProductMapper
    {
        public ProductModel MapToModel(RequestProductDTO requestProductDTO)
        {
            if(requestProductDTO == null)
            {
                Log.Error("A requisição não pode ser vazia");
                throw new ArgumentNullException(nameof(requestProductDTO));
            }

            return new ProductModel
            {
                Name = requestProductDTO.Name,
                Quantity = requestProductDTO.Quantity,
                Price = requestProductDTO.Price,
                Validation = requestProductDTO.Validation,
            }; 
        }


        public RequestProductDTO MapToDTO(ProductModel productModel)
        {
            if (productModel == null) 
            {
                Log.Error("A requisição não pode ser vazia");
                throw new ArgumentNullException(nameof(productModel));
            }

            return new RequestProductDTO
            {
                Name = productModel.Name,
                Quantity = productModel.Quantity,
                Price = productModel.Price,
                Validation = productModel.Validation,
            };
        }
    }
}
