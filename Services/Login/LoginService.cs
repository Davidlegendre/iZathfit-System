using Domain.Persona;
using Domain.Usuario;
using Models;
using Models.DTOS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Login
{
    public class LoginService : ILoginService
    {
        readonly IUsuarioRepository _user;
        readonly IPersonaRepository _persona;
        public LoginService(IUsuarioRepository user, IPersonaRepository personaRepository)
        {
            _user = user;
            _persona = personaRepository;
        }

        public async Task<UsuarioSistema?> Login(string user, string password)
        {
            var contra = EncryptManagementService.EncryptManagementService.Encrypt(password);
            if(string.IsNullOrWhiteSpace(contra)) { return null; }
            var idpersona = await _user.VerificarLogin(user, contra);
            if (idpersona != null)
            {
                var persona = await _persona.GetPersonaData(idpersona);
                return persona;
            }

            return null;

        }

        

    }
}
