using Configuration;
using Dapper;
using Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.OcupacionXPersona
{
    public class OcupacionXPersonaRepository : IOcupacionXPersonaRepository
    {
        IGeneralConfiguration _config;
        public OcupacionXPersonaRepository(IGeneralConfiguration config)
        {
            _config = config;
        }

        public async Task<List<Ocupacion>?> OcupacionPorPersona(Guid? idPersona) {
            using (var con = new SqlConnection(_config.GetConnection()))
            {
                var result = await con.QueryAsync<Ocupacion>("OcupacionPorPersona", new
                {
                    @idPersona = idPersona
                }, commandType: System.Data.CommandType.StoredProcedure);
                await con.CloseAsync();
                return result.Count() == 0 ? null:  result.AsList();
            }
        }
    }
}
