using System.ComponentModel.DataAnnotations;

namespace SistemaDeCadastro.Models.ModelsDTO
{
    public class RequestLoginDTO
    {
        [Required]
        public string UserName { get; set; }
        [Required]
        public string Password { get; set; }

    }
}
