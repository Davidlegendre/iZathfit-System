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
        Dictionary<TypeRol, string> TipoRoles = new Dictionary<TypeRol, string>()
        {
            { TypeRol.Desarrollador, "DEVP" },
            { TypeRol.Dueño, "DUNO" },
            { TypeRol.Administrador, "ADMN" },
            { TypeRol.Cliente, "CLNT" }
        };
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

        public string GetRol(TypeRol typerol) {
            return TipoRoles[typerol];
        }
        
    }

    public enum TypeRol
    { 
        Desarrollador,
        Dueño,
        Administrador,
        Cliente
    }
}
