using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Usuario
{
    public interface IUsuarioService
    {
        public Task<int> CambiarContraseña(string contraseña, Guid? idPersona);
        public Task<List<Models.Usuario>?> GetUsuarios();
        public Task<Models.Usuario?> InsertarUsuario(Models.Usuario? usuario);
        public Task<Models.Usuario?> UpdateUsuario(Models.Usuario? usuario);
        public Task<int> InactivarUsuario(Guid idusuario);
        public Task<Models.Usuario?> EliminarUsuario(Guid idUsuario);
    }
}
