using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Serilog;
using SistemaDeCadastro.Models.ModelsDTO;

namespace SistemaDeCadastro.Validation
{
    public static class ProductValidation
    {
        public static void Validate(RequestProductDTO requestProductDTO)
        {
            if (requestProductDTO == null)
            {
                Log.Error("Validação falohu:{ PropertyName} é nulo", nameof(requestProductDTO));
                throw new ArgumentNullException(nameof(requestProductDTO));
            }

            if (string.IsNullOrWhiteSpace(requestProductDTO.Name))
            {
                Log.Error("Validação falhou: O nome do produto não pode ser vazio.");
                throw new ArgumentNullException(nameof (requestProductDTO.Name));
            }

            if (requestProductDTO.Price <= 0)
            {
                Log.Error("Validação falhou: O preço não pode ser menor do que 0");
                throw new ArgumentException(nameof(requestProductDTO.Price));
            }

            if (requestProductDTO.Validation < DateOnly.FromDateTime(DateTime.Now))
            {
                Log.Error("Validação falhou: A data de validade deve ser uma data futura");
                throw new ArgumentException(nameof(requestProductDTO.Validation));
            }

            if (requestProductDTO.Quantity < 0 || requestProductDTO.Quantity == 0)
            {
                Log.Error("Validação falhou: A quantidade do produto não pode ser negativa, ou igual a 0");
                throw new ArgumentException(nameof(requestProductDTO.Quantity));
            }
        }
    }
}
