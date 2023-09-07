using Configuration;
using Dapper;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Usuario
{
    public class UsuarioRepository : IUsuarioRepository
    {
        readonly IGeneralConfiguration _config;
        public UsuarioRepository(IGeneralConfiguration config)
        {
            _config = config;
        }

        public async Task<Guid?> VerificarLogin(string user, string password) {
            using (var con = new SqlConnection(_config.GetConnection()))
            {
                var result = await con.ExecuteScalarAsync<Guid?>("IniciarSesion", new {
                    @user = user,
                    @password = password
                }, commandType: System.Data.CommandType.StoredProcedure);
                return result;  
            }
        }
    }
}
