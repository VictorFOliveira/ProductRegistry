using Serilog;
using SistemaDeCadastro.Models.ModelsDTO;

namespace SistemaDeCadastro.Validation
{
    public class ValidationUser
    {

        public static void Validate(RequestUserDTO requestUserDTO)
        {
            if (requestUserDTO == null) {
                Log.Error("Validação nula: {PropertyName} é nulo", nameof(requestUserDTO));
                throw new ArgumentNullException(nameof(requestUserDTO));
            }

            if (requestUserDTO.UserName == null)
            {
                Log.Error("Validação nula: O nome do usuário não pode ser nulo", nameof(requestUserDTO.UserName));
                throw new ArgumentNullException(nameof(requestUserDTO.UserName));
            }
            if (requestUserDTO.PasswordHash == null || requestUserDTO.PasswordHash.Length < 8)
            {
                Log.Error("Validação nula: O usuário precisa de uma senha de até 8 caracteres");
                throw new ArgumentNullException(nameof(requestUserDTO.PasswordHash));
            }
            if (requestUserDTO.Acesso == null)
            {
                Log.Error("Validação nula: O usuário precisa de um acesso");
                throw new ArgumentNullException(nameof(requestUserDTO.Acesso));
            }
        }
    }
}
