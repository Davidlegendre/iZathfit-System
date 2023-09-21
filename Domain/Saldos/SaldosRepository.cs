using Configuration;
using Dapper;
using Models.DTOS;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Saldos
{
    public class SaldosRepository : ISaldosRepository
    {
        IGeneralConfiguration _config;
        public SaldosRepository(IGeneralConfiguration config)
        {
            _config = config;
        }

        public async Task<SaldosEstadisticasDTO?> GetEstadisticas(Guid idpersona) {
            using (var con = new SqlConnection(_config.GetConnection()))
            {
                var result = await con.QueryFirstAsync<SaldosEstadisticasDTO?>("EstadisticasSaldo",
                    new { @idpersona =  idpersona}, commandType: System.Data.CommandType.StoredProcedure);
                await con.CloseAsync();
                return result;
            }
        }
    }
}
