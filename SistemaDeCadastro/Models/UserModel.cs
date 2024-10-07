using SistemaDeCadastro.Enum;
using System.ComponentModel.DataAnnotations;

namespace SistemaDeCadastro.Models
{
    public class UserModel
    {
        [Key]
        public int Id { get; set; }
        public string UserName { get; set; }
        public string PasswordHash { get; set; }
        public Acesso acesso { get; set; }
    }
}
