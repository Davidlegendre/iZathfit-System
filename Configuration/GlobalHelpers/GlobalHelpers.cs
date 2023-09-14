using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Configuration.GlobalHelpers
{
    public class GlobalHelpers : IGlobalHelpers
    {
        IGeneralConfiguration _config;
        public GlobalHelpers(IGeneralConfiguration config)
        {
            _config= config;
        }
        public List<RolModel>? FiltrarRoles(List<RolModel>? rolModels)
        {
            /*Dueño, Developer, Administrador
             Developer => Dueño, Administrador, cliente
             Dueño => Administrador, cliente
             Administrador => cliente
            Cliente => no tiene usuario
             */
            if (rolModels == null) return rolModels;
            
            if (PolicyReturnBool(TypeRol.Desarrollador))
                return rolModels.Where(x => x.Code != _config.GetRol(TypeRol.Desarrollador)).ToList();

            if (PolicyReturnBool(TypeRol.Dueño))
                return rolModels.Where(x => x.Code != _config.GetRol(TypeRol.Desarrollador) 
                && x.Code != _config.GetRol(TypeRol.Dueño)).ToList();

            if (PolicyReturnBool(TypeRol.Administrador))
                return rolModels.Where(x => x.Code != _config.GetRol(TypeRol.Desarrollador)
                && x.Code != _config.GetRol(TypeRol.Dueño) && x.Code != _config.GetRol(TypeRol.Administrador)).ToList();

            return null;
        }

        public bool IsNumber(string? numtexto)
        { 
            bool result = true;
            if(string.IsNullOrWhiteSpace(numtexto)) return false;
            foreach(var c in numtexto)
            {
                if (!char.IsNumber(c))
                { 
                   result = false; break;
                }
            }
            return result;
        }

        public void Policy(params TypeRol[] typeRols) {
            var user = _config.getuserSistema();
            if (user == null || user.CodeRol == null) throw new Exception("No es autorizado");
            if (!typeRols.ToList().Exists(x => x == _config.GetRol(user.CodeRol)))
                throw new Exception("No es autorizado");
        }

        public bool PolicyReturnBool(params TypeRol[] typeRols)
        {
            var user = _config.getuserSistema();
            if (user == null || user.CodeRol == null) return false;
            if (!typeRols.ToList().Exists(x => x == _config.GetRol(user.CodeRol)))
                return false;

            return true;
        }
    }
}
