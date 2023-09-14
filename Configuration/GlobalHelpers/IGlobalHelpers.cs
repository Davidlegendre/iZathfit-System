using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Configuration.GlobalHelpers
{
    public interface IGlobalHelpers
    {
        public List<RolModel>? FiltrarRoles(List<RolModel>? rolModels);
        public bool IsNumber(string? numtexto);
        public void Policy(params TypeRol[] typeRols);
        public bool PolicyReturnBool(params TypeRol[] typeRols);
    }
}
