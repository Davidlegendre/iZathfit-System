using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Usuario
{
    public interface IUsuarioRepository
    {
        public Task<Guid?> VerificarLogin(string user, string password);
        public Task<int> CambiarContraseña(string contraseña, Guid? IDPersona);
        public Task VerificarActivo(Guid? id);
        public Task<List<Models.Usuario>?> GetUsuarios();
        public Task<Models.Usuario?> InsertarUsuario(Models.Usuario? usuario);
        public Task<Models.Usuario?> UpdateUsuario(Models.Usuario? usuario);
        public Task<int> InactivarUsuario(Guid idusuario);
        public Task<Models.Usuario?> EliminarUsuario(Guid idUsuario);
    }
}
