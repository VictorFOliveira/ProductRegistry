using System.ComponentModel;

namespace SistemaDeCadastro.Enum
{
    public enum Acesso
    {
        /// <summary>
        /// Acesso total ao sistema
        /// </summary>
        [Description("Acesso total ao sistema")]
        Admin = 0,

        /// <summary>
        /// Acesso comum ao sistema
        /// </summary>
        [Description("Acesso comum ao sistema")]
        Regular = 1,

        /// <summary>
        /// Usuário sem acesso
        /// </summary>
        [Description("Usuário sem acesso")]
        SemAcesso = 2
    }
}
