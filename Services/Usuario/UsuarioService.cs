using Domain.Usuario;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Usuario
{
    public class UsuarioService : IUsuarioService
    {
        IUsuarioRepository _repo;
        public UsuarioService(IUsuarioRepository repo)
        {
            _repo = repo;
        }

        public async Task<int> CambiarContraseña(string contraseña, Guid? idPersona)
        { 

            return await _repo.CambiarContraseña(contraseña, idPersona);
        }
    }
}
