using Domain.Usuario;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
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

        public async Task<List<Models.Usuario>?> GetUsuarios() { 
            return await _repo.GetUsuarios();
        }

        public async Task<Models.Usuario?> InsertarUsuario(Models.Usuario? usuario)
        {
            return await _repo.InsertarUsuario(usuario);
        }

        public async Task<Models.Usuario?> UpdateUsuario(Models.Usuario? usuario)
        { 
            return await _repo.UpdateUsuario(usuario);
        }

        public async Task<int> InactivarUsuario(Guid idusuario)
        {
            return await _repo.InactivarUsuario(idusuario);
        }
        public async Task<Models.Usuario?> EliminarUsuario(Guid idUsuario)
        {
            return await _repo.EliminarUsuario(idUsuario);
        }
    }
}
