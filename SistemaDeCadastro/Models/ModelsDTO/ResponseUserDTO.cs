using SistemaDeCadastro.Enum;

namespace SistemaDeCadastro.Models.ModelsDTO
{
    public class ResponseUserDTO
    {
        public int Id { get; set; }
        public string UserName { get; set; }
        public Acesso acesso { get; set; }
    }
}
