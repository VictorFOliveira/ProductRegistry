using SistemaDeCadastro.Enum;
using System.ComponentModel.DataAnnotations;

namespace SistemaDeCadastro.Models
{
    public class UserModel
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string UserName { get; set; }
        [Required]
        public string PasswordHash { get; set; }

        public Acesso acesso { get; set; }
        public string Matricula { get; set; }


        //Atributos para auditoria

        public DateTime DateCreate { get; set; } = DateTime.Now;
        public DateTime? DateAlteration { get; set; } = null;
        public string UltimaAlteracaoPor { get; set; }

    }
}
