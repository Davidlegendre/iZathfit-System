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
    }
}
