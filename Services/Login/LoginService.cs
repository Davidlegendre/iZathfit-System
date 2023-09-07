using Domain.Persona;
using Domain.RolXPersona;
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
        readonly IRolXPersonaRepository _rolxpersona;
        public LoginService(IUsuarioRepository user, IPersonaRepository personaRepository,
            IRolXPersonaRepository rolXPersonaRepository)
        {
            _user = user;
            _persona = personaRepository;
            _rolxpersona = rolXPersonaRepository;
        }

        public async Task<UsuarioSistema?> Login(string user, string password)
        {
            var idpersona = await _user.VerificarLogin(user, password);
            if (idpersona != null)
            {
                var persona = await _persona.GetPersonaData(idpersona);
                var roles = await _rolxpersona.GetRols(idpersona);
                persona!.Roles = roles;
                return persona;
            }

            return null;

        }

    }
}
