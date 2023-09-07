using Models.DTOS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Configuration
{
    public class GeneralConfiguration : IGeneralConfiguration
    {
        
        readonly string? connection = Environment.GetEnvironmentVariable("CONBDZATHFIT");
        
        public string? GetConnection()
        {
            return connection;
        }

        UsuarioSistema? UsuarioSistema;
        public void SetUserSistema(UsuarioSistema? usuario)
        { 
            UsuarioSistema = usuario;
        }
        public UsuarioSistema? getuserSistema() => UsuarioSistema;

        
    }
}
