using Models;
using Models.DTOS;
using Models.ModelsCommons;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Text.RegularExpressions;
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

        public bool IsNullOrWhiteSpaceAndNumber(string? numtexto, bool IsOptional = false)
        { 
            bool result = true;
            if (string.IsNullOrWhiteSpace(numtexto) && !IsOptional) return false;
            if (IsOptional && string.IsNullOrWhiteSpace(numtexto)) return true;
            foreach (var c in numtexto)
            {
                if (!char.IsNumber(c))
                {
                    result = false; break;
                }
            }
            return result;
        }

        public bool IsNullOrWhiteSpaceAndDecimal(string? value, bool IsOptional = false)
        {
            if (string.IsNullOrWhiteSpace(value) && !IsOptional) return false;
            if (IsOptional && string.IsNullOrWhiteSpace(value)) return true;
            decimal outdecimal;
            return decimal.TryParse(value,out outdecimal);

        }
        public bool IsNullOrWhiteSpaceAndEmail(string? value, bool IsOptional = false)
        {
            if (string.IsNullOrWhiteSpace(value) && !IsOptional) return false;
            if (IsOptional && string.IsNullOrWhiteSpace(value)) return true;
            return Regex.IsMatch(value.ToLower(), @"^[_a-z0-9-]+(\.[_a-z0-9-]+)*@[a-z0-9-]+(\.[a-z0-9-]+)*(\.[a-z]{2,4})$");
        }

        public bool IsInLength(string? value, int numLength)
        { 
            return value?.Length == numLength;
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

        public int ColumnsFromWidthWindow(int ActuaWidthWindow) {
            return ActuaWidthWindow / 270;
        }

        public LinkModel[] GetLinksContacts()
        {
            return new Models.ModelsCommons.LinkModel[]
                        {
                        new Models.ModelsCommons.LinkModel()
                        {
                            TitlePage = "Dev. David",
                            Url = "https://api.whatsapp.com/send?phone=51914847720"
                        },
                        new Models.ModelsCommons.LinkModel()
                        {
                            TitlePage = "Dev. Francois",
                            Url = "https://api.whatsapp.com/send?phone=51998440211"
                        }
                        };
        }

        public string TransformMonthsToString(int Months)
        {
            //10 Años y 2 Meses
            if (Months < 12) return Months + ((Months == 1) ? " Mes" : " Meses");
            if (Months == 12) return "1 Año";
            var FechaFutura = DateTime.Now.AddMonths(Months).Date;
            var FechaActual = DateTime.Now.Date;
            var years = FechaFutura.Year - FechaActual.Year;
            years = FechaFutura.Month <= FechaActual.Month ? years - 1 : years;
            var meses = Months % 12;
            return years + ((years == 1) ? " Año" : " Años") + (meses > 0 ? (meses == 1 ? " " + meses + " Mes" : " " + meses + " Meses") : "");

        }

        public (bool success, string messaje) IsNullOrWhiteSpaceAndCorrectPassword(string input) {
            if (string.IsNullOrWhiteSpace(input)) return (false, "texto esta vacio");
            if (input.Length < 8) return (false, "La contraseña debe tener minimo 8 caracteres");
            var hasNumber = new Regex(@"[0-9]+");
            var hasUpperChar = new Regex(@"[A-Z]+");
            var hasSymbols = new Regex(@"[!@#$%^&*()_+=\[{\]};:<>|./?,-]");

            var isValidated = hasNumber.IsMatch(input) && hasUpperChar.IsMatch(input) && hasSymbols.IsMatch(input);

            return (isValidated, isValidated ? "": "La Contraseña Requiere numeros, letras mayusculas - minusculas y simbolos");
        }

        public int CorrectLengthForIdentity(string? identity) {
            if(string.IsNullOrWhiteSpace(identity)) return -1;
            var lengthofidentity = identity?.ToLower() switch
            {
                "dni" => 8,
                "ce" => 12,
                "ps" => 12,
                _ => 0
            };
            return lengthofidentity;
        }
    }
}
