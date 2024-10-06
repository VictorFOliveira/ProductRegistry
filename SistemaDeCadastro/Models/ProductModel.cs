using System.ComponentModel.DataAnnotations;

namespace SistemaDeCadastro.Models
{
    public class ProductModel
    {


        public int Id { get; set; }

        [Required(ErrorMessage = "O nome do produto é obrigatório.")]
        public string Name { get; set; }

        [Range(0, int.MaxValue, ErrorMessage = "A quantidade deve ser maior ou igual a zero.")]
        public int Quantity { get; set; }

        [Range(0.01, double.MaxValue, ErrorMessage = "O preço deve ser maior que zero.")]
        public decimal Price { get; set; }

        public DateOnly Validation { get; set; }


    }
}
