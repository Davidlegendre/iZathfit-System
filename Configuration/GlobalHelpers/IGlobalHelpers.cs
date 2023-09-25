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
        public bool IsNullOrWhiteSpaceAndNumber(string? numtexto, bool IsOptional = false);
        public void Policy(params TypeRol[] typeRols);
        public bool PolicyReturnBool(params TypeRol[] typeRols);
        public int ColumnsFromWidthWindow(int ActuaWidthWindow);
        public LinkModel[] GetLinksContacts();
        public bool IsNullOrWhiteSpaceAndDecimal(string? value, bool IsOptional = false);
        public string TransformMonthsToString(int Months);
        public bool IsNullOrWhiteSpaceAndEmail(string? value, bool IsOptional = false);
        public bool IsInLength(string? value, int numLength);
    }
}
