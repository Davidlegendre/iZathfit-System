using Models.Login;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    public class LoginService
    {
        public UserModel? Login(string user, string password) {
            if (user != "david" || password != "12345")
            {
                return null;
            }
            return new UserModel()
            {
                Id = Guid.NewGuid(),
                Apellidos = "Legendre",
                Nombres = "David",
                Roles = new List<Models.RolModel>() { new Models.RolModel() { Id = 1, Description = "haosdhas", Code = "ADDN" } }
            };
        }
      
    }
}
