using Models.DTOS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Login
{
    public interface ILoginService
    {
        public Task<UsuarioSistema?> Login(string user, string password);
    }
}
