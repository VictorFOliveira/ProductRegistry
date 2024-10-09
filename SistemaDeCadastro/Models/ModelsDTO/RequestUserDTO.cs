using SistemaDeCadastro.Enum;
using System.ComponentModel.DataAnnotations;

namespace SistemaDeCadastro.Models.ModelsDTO
{
    public class RequestUserDTO
    {
        [Required]
        public string UserName { get; set; }
        [Required]
        public string PasswordHash { get; set; }
        public Acesso  Acesso { get; set; }

    }
}
