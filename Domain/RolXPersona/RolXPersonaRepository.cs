using Configuration;
using Dapper;
using Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.RolXPersona
{
    public class RolXPersonaRepository : IRolXPersonaRepository
    {
        readonly IGeneralConfiguration _generalConfiguration;
        public RolXPersonaRepository(IGeneralConfiguration generalConfiguration)
        {
            _generalConfiguration = generalConfiguration;
        }

        public async Task<List<Rol>?> GetRols(Guid? IdPersona)
        {
            using (var con = new SqlConnection(_generalConfiguration.GetConnection()))
            {
                var result = await con.QueryAsync<Rol>("RolesPorPersona", 
                    new { @idPersona = IdPersona },
                    commandType: System.Data.CommandType.StoredProcedure);

                return result != null ? result.ToList(): null;
            }
        }
    }
}
