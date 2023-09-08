using Domain.OcupacionXPersona;
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
        readonly IOcupacionXPersonaRepository _ocupacion;
        public LoginService(IUsuarioRepository user, IPersonaRepository personaRepository, IOcupacionXPersonaRepository ocupacion)
        {
            _user = user;
            _persona = personaRepository;
            _ocupacion= ocupacion;
        }

        public async Task<UsuarioSistema?> Login(string user, string password)
        {
            var idpersona = await _user.VerificarLogin(user, password);
            await _user.VerificarActivo(idpersona);
            if (idpersona != null)
            {
                var persona = await _persona.GetPersonaData(idpersona);
                var ocupaciones = await _ocupacion.OcupacionPorPersona(idpersona);
                persona!.Ocupaciones = ocupaciones;
                return persona;
            }

            return null;

        }

        

    }
}
