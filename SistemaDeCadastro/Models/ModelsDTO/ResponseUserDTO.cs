using SistemaDeCadastro.Enum;
using System.ComponentModel.DataAnnotations;

namespace SistemaDeCadastro.Models.ModelsDTO
{
    public class ResponseUserDTO
    {
        public int Id { get; set; }
        public string UserName { get; set; }
        public Acesso acesso { get; set; }

        //Atributos para auditoria
        public DateTime DateCreate { get; set; } = DateTime.Now;
        public DateTime? DateAlteration { get; set; }
        public string Matricula { get; set; }
        public string UltimaAlteracaoPor { get; set; }

    }
}
