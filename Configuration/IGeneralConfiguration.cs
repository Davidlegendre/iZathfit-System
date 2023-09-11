using Models.DTOS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Configuration
{
    public interface IGeneralConfiguration
    {
        public string? GetConnection();
        public void SetUserSistema(UsuarioSistema? usuario);
        public UsuarioSistema? getuserSistema();
        public string GetRol(TypeRol typerol);
    }
}
