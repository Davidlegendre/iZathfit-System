using Models;
using Models.DTOS;
using Models.ModelsCommons;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
        public int ColumnsFromWidthWindow(int ActuaWidthWindow);
        public LinkModel[] GetLinksContacts();
        public string TransformMonthsToString(int Months);
    }
}
